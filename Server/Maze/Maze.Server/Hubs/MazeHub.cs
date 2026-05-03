using Maze.Common;
using Maze.Common.Types;
using Maze.Core.ConnectionRegistry;
using Maze.Core.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Maze.Server.Hubs;

public class MazeHub : Hub
{
    private readonly GameService _gameService;
    private readonly IConnectionRegistry _registry;
    private readonly TokenService _tokenService;

    public MazeHub(GameService gameService, IConnectionRegistry connectionRegistry, TokenService tokenService)
    {
        _gameService = gameService;
        _registry = connectionRegistry;
        _tokenService = tokenService;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();

        // .NET SignalR client sends via Authorization header; browsers use access_token query param
        var authHeader = httpContext?.Request.Headers["Authorization"].FirstOrDefault();
        string? token = authHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true
            ? authHeader["Bearer ".Length..]
            : httpContext?.Request.Query["access_token"].FirstOrDefault();

        if (string.IsNullOrEmpty(token))
            throw new HubException("No token provided");

        var playerId = await _tokenService.ValidateAsync(token);
        _registry.Register(playerId, Context.ConnectionId);
        Context.Items["PlayerId"] = playerId;

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _registry.Unregister(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    [HubMethodName(Constants.HubMethods.CreateGame)]
    public string CreateGame(int rows, int cols)
    {
        return JsonSerializer.Serialize(_gameService.CreateGame(rows, cols), JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.SetUser)]
    public string SetUser(EntityId gameId, int startRow, int startCol)
    {
        var player = GetPlayerId();
        return JsonSerializer.Serialize(_gameService.SetUser(gameId, player, startRow, startCol), JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.Move)]
    public string Move(EntityId gameId, MoveDirection direction, EntityId? keyId = null)
    {
        var player = GetPlayerId();
        return JsonSerializer.Serialize(_gameService.Move(gameId, player, direction, keyId), JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.DestroyWall)]
    public string DestroyWall(EntityId gameId, MoveDirection direction)
    {
        var player = GetPlayerId();
        return JsonSerializer.Serialize(_gameService.DestroyWall(gameId, player, direction), JsonOptions.Default);
    }

    private PlayerId GetPlayerId() =>
        Context.Items["PlayerId"] is PlayerId p ? p : throw new HubException("Not authenticated");
}
