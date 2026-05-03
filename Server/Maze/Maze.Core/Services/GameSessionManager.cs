using Maze.Common.Types;
using Maze.Core.Models;
using Maze.MazeStructure.MazeGenerators;
using Maze.MazeStructure.MazeGenerators.Decorators;
using Maze.MazeStructure.MazeGenerators.Generators;
using Maze.MazeStructure.Topology;
using System.Collections.Concurrent;

namespace Maze.Core.Services
{
    public class GameSessionManager
    {
        private readonly ConcurrentDictionary<EntityId, GameSession> _sessions = new ConcurrentDictionary<EntityId, GameSession>();

        internal GameSession? Get(EntityId gameId) => _sessions.TryGetValue(gameId, out var session) ? session : null;

        public GameSessionManager()
        {
        }

        internal (EntityId gameId, GameSession session) CreateWithId(int rows, int cols)
        {
            var mazeTopology = new GridTopology(rows, cols);
            var mazeBuilder = new SimpleMazeBuilder();
            IMazeGenerator generator = new RecursiveBacktrackerGenerator(mazeBuilder, mazeTopology);
            // TODO: make configurable in the future
            generator = new BraidMazeGenerator(generator, mazeBuilder, 0.5f);
            generator = new KeyPlacementGenerator(generator, mazeBuilder, keyCount: 4);
            var mazeInfo = generator.Generate();
            var world = new Maze.GameWorld.GameWorld();
            var mazeId = world.RegisterMaze(mazeInfo);

            var session = new GameSession(world, mazeId, mazeTopology);
            var gameId = EntityId.New();
            _sessions[gameId] = session;

            return (gameId, session);
        }
    }
}
