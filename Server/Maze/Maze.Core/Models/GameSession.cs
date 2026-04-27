using Maze.MazeStructure;
using Maze.MazeStructure.Topology;

namespace Maze.Core.Models
{
    internal class GameSession
    {
        public GameWorld.GameWorld World { get; }
        public IMazeInfo MazeInfo { get; }
        public IMazeTopology Topology { get; }

        public GameSession(GameWorld.GameWorld world, IMazeInfo mazeInfo, IMazeTopology mazeTopology)
        {
            World = world;
            MazeInfo = mazeInfo;
            Topology = mazeTopology;
        }
    }
}
