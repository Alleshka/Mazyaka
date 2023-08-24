using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal class DestroyedMazeWall : BaseMazeSite, IMazeWall
    {
        public bool CanDestroy => false;

        public bool IsDestroyed => true;

        private IMazeSite _next;

        public DestroyedMazeWall(IMazeSite next)
        {
            _next = next;
        }

        public override MoveResult Enter(IMazePlayer player)
        {
            var result = _next.Enter(player);
            result.MazeSite += this.GetType().Name;
            return result;
        }
    }
}
