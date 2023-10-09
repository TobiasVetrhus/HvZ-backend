using HvZ_backend.Data.Entities;
using HvZ_backend.Services.Games;
using HvZ_backend.Services.Locations;
using HvZ_backend.Services.Messages;
using HvZ_backend.Services.Players;
using HvZ_backend.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

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

            RoleClaimType = "roles",
            ValidIssuers = new List<string>
              {
                Configuration["TokenSecrets:IssuerURI"]
              }
        };
    });

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<HvZDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HvZDb"));
});

// Add custom services
builder.Services.AddScoped<IGameService, GameService>();
//builder.Services.AddScoped<IConversationService, ConversationService>();
//builder.Services.AddScoped<IKillService, KillService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
//builder.Services.AddScoped<IMissionService, MissionService>();
//builder.Services.AddScoped<IPlayerService, PlayerService>();
//builder.Services.AddScoped<IRuleService, RuleService>();
//builder.Services.AddScoped<ISquadService, SquadService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();



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

app.MapControllers();

app.Run();
