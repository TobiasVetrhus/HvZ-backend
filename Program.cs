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

var builder = WebApplication.CreateBuilder(args);
var Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();


builder.Services.AddSignalR();

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
                //Retrieves the keys from keycloak instance to verify token
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




// Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Use CORS with your custom policy
app.UseCors("MyCorsPolicy");

app.MapControllers();

app.MapHub<ChatHub>("/chathub").RequireCors("MyCorsPolicy"); ;

app.Run();
