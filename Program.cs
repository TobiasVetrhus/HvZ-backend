using HvZ_backend.Data.Entities;
using HvZ_backend.Services.Games;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HvZDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HvZDb"));
});

// Add custom services
builder.Services.AddScoped<IGameService, GameService>();
//builder.Services.AddScoped<IConversationService, ConversationService>();
//builder.Services.AddScoped<IKillService, KillService>();
//builder.Services.AddScoped<ILocationService, LocationService>();
//builder.Services.AddScoped<IMessageService, MessageService>();
//builder.Services.AddScoped<IMissionService, MissionService>();
//builder.Services.AddScoped<IPlayerService, PlayerService>();
//builder.Services.AddScoped<IRuleService, RuleService>();
//builder.Services.AddScoped<ISquadService, SquadService>();
//builder.Services.AddScoped<IUserService, UserService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
