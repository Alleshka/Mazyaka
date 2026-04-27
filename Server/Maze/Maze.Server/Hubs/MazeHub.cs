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

    public MazeHub(GameService gameService, IConnectionRegistry connectionRegistry)
    {
        _gameService = gameService;
        _registry = connectionRegistry;
    }

    public override async Task OnConnectedAsync()
    {
        var playerId = GetPlayerIdFromContext();
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
    public string Move(EntityId gameId, MoveDirection direction)
    {
        var player = GetPlayerId();
        return JsonSerializer.Serialize(_gameService.Move(gameId, player, direction), JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.DestroyWall)]
    public string DestroyWall(EntityId gameId, MoveDirection direction)
    {
        var player = GetPlayerId();
        return JsonSerializer.Serialize(_gameService.DestroyWall(gameId, player, direction), JsonOptions.Default);
    }

    private PlayerId GetPlayerIdFromContext()
    {
        var http = Context.GetHttpContext();

        var authHeader = http?.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader))
            throw new UnauthorizedAccessException("No authorization header");

        // Strip "Bearer " prefix
        var token = authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..]
            : authHeader;

        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("No player ID provided");

        return PlayerId.From(token);
    }

    private PlayerId GetPlayerId() =>
    Context.Items["PlayerId"] is PlayerId p ? p : throw new HubException("Not authenticated");
}
