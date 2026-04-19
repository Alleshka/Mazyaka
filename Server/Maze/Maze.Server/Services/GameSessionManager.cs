using Maze.MazeStructure.MazeGenerators;
using Maze.Server.Models;
using System.Collections.Concurrent;

namespace Maze.Server.Services;

public class GameSessionManager
{
    private readonly ConcurrentDictionary<Guid, GameSession> _sessions = new();

    public (Guid gameId, GameSession session) CreateWithId(int rows, int cols)
    {
        var generator = new GridMazeGenerator(rows, cols);
        var mazeInfo = generator.Generate();
        var world = new Maze.GameWorld.GameWorld();

        var session = new GameSession(world, mazeInfo, rows, cols);
        var gameId = Guid.NewGuid();
        _sessions[gameId] = session;

        return (gameId, session);
    }

    public GameSession? Get(Guid gameId) =>
        _sessions.TryGetValue(gameId, out var session) ? session : null;
}
