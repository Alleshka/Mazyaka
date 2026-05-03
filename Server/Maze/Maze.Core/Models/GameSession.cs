using Maze.Common.Types;
using Maze.MazeStructure.Topology;

namespace Maze.Core.Models
{
    internal class GameSession
    {
        public GameWorld.GameWorld World { get; }
        public EntityId MazeId { get; }
        public IMazeTopology Topology { get; }

        public GameSession(GameWorld.GameWorld world, EntityId mazeId, IMazeTopology mazeTopology)
        {
            World = world;
            MazeId = mazeId;
            Topology = mazeTopology;
        }
    }
}
