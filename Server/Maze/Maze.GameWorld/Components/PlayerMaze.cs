using Maze.MazeStructure;

namespace Maze.GameWorld.Components
{
    internal struct PlayerMaze
    {
        public IMazeInfo MazeInfo { get; init; }

        public PlayerMaze(IMazeInfo mazeInfo)
        {
            MazeInfo = mazeInfo;
        }
    }
}
