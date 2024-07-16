using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal class SimpleMazeWall : BaseMazeConnection, IMazeWall
    {
        public override bool CanDestroy => true;

        public override MoveResult Enter(IMazePlayer player, MoveDirection direction)
        {
            if (!IsDestroyed)
            {
                var prev = this[direction.Opposite()];

                return new MoveResult()
                {
                    Status = MoveStatus.Failure,
                    MazeSite = this.GetType().Name,

                    // TODO: Do I really need to return address even if failure?
                    Point = new MazePoint(prev.Row, prev.Column)
                };
            }
            else
            {
                return base.Enter(player, direction);
            }
        }
    }
}
