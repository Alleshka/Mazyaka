using Maze.Server.Hubs;
using Maze.Server.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<GameSessionManager>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
app.MapHub<MazeHub>("/mazehub");

app.Run();
