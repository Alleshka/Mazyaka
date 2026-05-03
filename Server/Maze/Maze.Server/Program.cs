using Maze.Common;
using Maze.Core.ConnectionRegistry;
using Maze.Core.Services;
using Maze.Server;
using Maze.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    })
    .AddJsonProtocol(option =>
    {
        JsonOptions.Configure(option.PayloadSerializerOptions);
    });

builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<IConnectionRegistry, InMemoryConnectionRegistry>();
builder.Services.AddSingleton<GameSessionManager>();
builder.Services.AddSingleton<TokenService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

app.MapPost("/api/token", (TokenService tokens) =>
{
    var (token, playerId) = tokens.Issue();
    return Results.Ok(new { token, playerId = playerId.ToString() });
});

app.MapHub<MazeHub>("/mazehub");

app.Run();
