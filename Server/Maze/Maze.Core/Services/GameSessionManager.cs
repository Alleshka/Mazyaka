using Maze.Common.Types;
using Maze.Core.Models;
using Maze.GameWorld.Services;
using Maze.MazeStructure.MazeGenerators;
using Maze.MazeStructure.Topology;
using System.Collections.Concurrent;

namespace Maze.Core.Services
{
    public class GameSessionManager
    {
        private readonly ConcurrentDictionary<EntityId, GameSession> _sessions = new ConcurrentDictionary<EntityId, GameSession>();
        private readonly ConnectionContextBuilder _connectionContextBuilder;

        internal GameSession? Get(EntityId gameId) => _sessions.TryGetValue(gameId, out var session) ? session : null;

        public GameSessionManager(ConnectionContextBuilder connectionContextBuilder)
        {
            _connectionContextBuilder = connectionContextBuilder;
        }

        internal (EntityId gameId, GameSession session) CreateWithId(int rows, int cols)
        {
            var mazeTopology = new GridTopology(rows, cols);
            var mazeBuilder = new SimpleMazeBuilder();
            IMazeGenerator generator = new RecursiveBacktrackerGenerator(mazeBuilder, mazeTopology);
            // TODO: make configurable in the future
            generator = new BraidMazeGenerator(generator, mazeBuilder, 0.5f);
            var mazeInfo = generator.Generate();
            var world = new Maze.GameWorld.GameWorld(_connectionContextBuilder);

            var session = new GameSession(world, mazeInfo, mazeTopology);
            var gameId = EntityId.New();
            _sessions[gameId] = session;

            return (gameId, session);
        }
    }
}
