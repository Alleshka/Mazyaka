using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal abstract class BaseMazeSite : IMazeSite
    {
        protected Dictionary<MoveDirection, IMazeSite> Sides;
        protected BaseMazeSite()
        {
            Sides = new Dictionary<MoveDirection, IMazeSite>();
        }

        public abstract MoveResult Enter(IMazePlayer player, MoveDirection direction);

    }
}
