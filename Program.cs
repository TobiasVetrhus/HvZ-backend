using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Hubs;
using HvZ_backend.Services.Conversations;
using HvZ_backend.Services.Games;
using HvZ_backend.Services.Kills;
using HvZ_backend.Services.Locations;
using HvZ_backend.Services.Messages;
using HvZ_backend.Services.Missions;
using HvZ_backend.Services.Players;
using HvZ_backend.Services.Rules;
using HvZ_backend.Services.Squads;
using HvZ_backend.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);
var Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Configure Serilog with the console sink
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Information("Application is starting... DEBUG");

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var client = new HttpClient();
                var keyuri = Configuration["TokenSecrets:KeyURI"];
                // Retrieves the keys from the Keycloak instance to verify the token
                var response = client.GetAsync(keyuri).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                return keys.Keys;
            },

            ValidIssuers = new List<string>
            {
                Configuration["TokenSecrets:IssuerURI"]
            }
        };
    });

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed((host) => true);
    });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<HvZDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HvZDb"));
});

// Add SignalR configuration
builder.Services.AddSignalR();

// Add custom services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IKillService, KillService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IRuleService, RuleService>();
builder.Services.AddScoped<ISquadService, SquadService>();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Redirect HTTP requests to HTTPS for secure communication.
app.UseHttpsRedirection();

// Enable authentication for user identity and authorization.
app.UseAuthentication();

// Enable authorization to control access to API resources.
app.UseAuthorization();

// Use CORS with your custom policy
app.UseCors("MyCorsPolicy");

// Map API controllers for routing HTTP requests to actions.
app.MapControllers();

// Map the ChatHub SignalR hub and require CORS policy "MyCorsPolicy"
app.MapHub<ChatHub>("/chathub").RequireCors("MyCorsPolicy");

// Map the LocationHub SignalR hub and require CORS policy "MyCorsPolicy"
app.MapHub<LocationHub>("/locationhub").RequireCors("MyCorsPolicy");

// Complete the request pipeline configuration.
app.Run();

