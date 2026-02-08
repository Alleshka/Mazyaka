using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic.Connections
{
    public class GameBoundary : GameWall
    {
        public override bool CanDestroy => false;
        public override bool Destroyed => false;

        public GameBoundary(IMazeConnection connection, bool destructable = true) : base(connection, destructable)
        {
        }
    }
}
