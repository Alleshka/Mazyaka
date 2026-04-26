using Maze.Server.Hubs;
using Maze.Server.Services;
using Maze.Common.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services
     .AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    })
    .AddJsonProtocol(option =>
    {
        option.PayloadSerializerOptions.Converters.Add(new EntityIdJsonConverter());
    });
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
