using Maze.Common.Types;
using Maze.MazeStructure.MazeGenerators;
using Maze.MazeStructure.Topology;
using Maze.Server.Models;
using System.Collections.Concurrent;

namespace Maze.Server.Services;

public class GameSessionManager
{
    private readonly ConcurrentDictionary<EntityId, GameSession> _sessions = new();

    public (EntityId gameId, GameSession session) CreateWithId(int rows, int cols)
    {
        var mazeTopology = new GridTopology(rows, cols);
        var mazeBuilder = new SimpleMazeBuilder();
        IMazeGenerator generator = new RecursiveBacktrackerGenerator(mazeBuilder, mazeTopology);
        // TODO: make configurable in the future
        generator = new BraidMazeGenerator(generator, mazeBuilder, 0.5f);
        var mazeInfo = generator.Generate();
        var world = new Maze.GameWorld.GameWorld();

        var session = new GameSession(world, mazeInfo, mazeTopology);
        var gameId = EntityId.New();
        _sessions[gameId] = session;

        return (gameId, session);
    }

    public GameSession? Get(EntityId gameId) =>
        _sessions.TryGetValue(gameId, out var session) ? session : null;
}
