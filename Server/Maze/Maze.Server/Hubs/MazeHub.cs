using System.Text.Json;
using System.Text.Json.Serialization;
using Maze.Common;
using Maze.Server.Services;
using Microsoft.AspNetCore.SignalR;
using Maze.Common.DTO;

namespace Maze.Server.Hubs;

public class MazeHub : Hub
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = null,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly GameSessionManager _sessions;

    public MazeHub(GameSessionManager sessions)
    {
        _sessions = sessions;
    }

    public string CreateGame(int rows, int cols)
    {
        var (gameId, _) = _sessions.CreateWithId(rows, cols);

        return JsonSerializer.Serialize(new CreateGameResponse
        {
            GameId = gameId,
            Rows = rows,
            Cols = cols
        }, JsonOptions);
    }

    public string SetUser(Guid gameId, int startRow, int startCol)
    {
        var session = _sessions.Get(gameId)
            ?? throw new HubException($"Game '{gameId}' not found.");

        int startRoomId = startRow * session.Cols + startCol;
        var userId = session.World.CreatePlayer(startRoomId);
        var cell = startRoomId;

        return JsonSerializer.Serialize(new PlayerJoinResponse
        {
            UserId = userId,
            CellId = cell
        }, JsonOptions);
    }

    public string Move(Guid gameId, string userId, MoveDirection direction)
    {
        var session = _sessions.Get(gameId)
            ?? throw new HubException($"Game '{gameId}' not found.");

        if (!Guid.TryParse(userId, out var playerId))
            throw new HubException($"Invalid userId '{userId}'.");

        var result = session.World.ExecuteMove(playerId, direction);

        if (result.Win)
            return JsonSerializer.Serialize(new MoveResponse { Success = true, Win = true }, JsonOptions);

        if (!result.SuccessMove)
        {
            return JsonSerializer.Serialize(new MoveResponse
            {
                Success = false,
                BlockedDirection = direction,
                MoveBlocker = new MoveBlocker()
                {
                    BlockerId = result?.BlockedBy?.Id ?? -1,
                    BlockedConnectionType = result?.BlockedBy?.Name
                }
            }, JsonOptions);
        }

        return JsonSerializer.Serialize(new MoveResponse
        {
            Success = true,
            CellId = result.RoomId // CellRevealBuilder.Build(result.RoomId, session)
        }, JsonOptions);
    }
}
