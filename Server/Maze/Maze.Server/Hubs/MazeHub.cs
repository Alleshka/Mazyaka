using System.Text.Json;
using System.Text.Json.Serialization;
using Maze.Common;
using Maze.Server.Services;
using Microsoft.AspNetCore.SignalR;
using Maze.Common.DTO;
using Maze.Common.Types;

namespace Maze.Server.Hubs;

public class MazeHub : Hub
{
    private readonly GameSessionManager _sessions;

    public MazeHub(GameSessionManager sessions)
    {
        _sessions = sessions;
    }

    [HubMethodName(Constants.HubMethods.CreateGame)]
    public string CreateGame(int rows, int cols)
    {
        var (gameId, _) = _sessions.CreateWithId(rows, cols);

        return JsonSerializer.Serialize(new CreateGameResponse
        {
            GameId = gameId,
            Rows = rows,
            Cols = cols
        }, JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.SetUser)]
    public string SetUser(int gameId, int startRow, int startCol)
    {
        EntityId id = EntityId.From(gameId);
        var session = _sessions.Get(id)
            ?? throw new HubException($"Game '{gameId}' not found.");

        EntityId startRoomId = session.Topology.GetRoomByCoordinates(startRow, startCol).Id;
        var userId = session.World.CreatePlayer(session.MazeInfo, startRoomId);
        var cell = startRoomId;

        return JsonSerializer.Serialize(new PlayerJoinResponse
        {
            UserId = userId,
            CellId = cell
        }, JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.Move)]
    public string Move(EntityId gameId, EntityId userId, MoveDirection direction)
    {
        // EntityId game = EntityId.From(gameId);
        var session = _sessions.Get(gameId)
            ?? throw new HubException($"Game '{gameId}' not found.");

        //EntityId user = EntityId.From(userId);
        var result = session.World.ExecuteMove(userId, direction);

        if (result.Win == true)
            return JsonSerializer.Serialize(new MoveResponse { Success = true, Win = true }, JsonOptions.Default);

        if (!result.SuccessMove)
        {
            return JsonSerializer.Serialize(new MoveResponse
            {
                Success = false,
                BlockedDirection = direction,
                MoveBlocker = new MoveBlocker()
                {
                    BlockerId = result?.BlockedBy?.Id ?? EntityId.Empty,
                    BlockedConnectionType = result?.BlockedBy?.Name
                }
            }, JsonOptions.Default);
        }

        return JsonSerializer.Serialize(new MoveResponse
        {
            Success = true,
            CellId = result.RoomId // CellRevealBuilder.Build(result.RoomId, session)
        }, JsonOptions.Default);
    }

    [HubMethodName(Constants.HubMethods.DestroyWall)]
    public string DestroyWall(int gameId, int userId, MoveDirection direction)
    {
        EntityId game = EntityId.From(gameId);
        var session = _sessions.Get(game)
            ?? throw new HubException($"Game '{game}' not found.");
        EntityId user = EntityId.From(userId);
        var result = session.World.ExecuteDestroyWall(user, direction);
        string json = JsonSerializer.Serialize(result, JsonOptions.Default);
        return json;
    }
}
