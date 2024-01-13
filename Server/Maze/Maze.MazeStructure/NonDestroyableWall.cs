using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal class NonDestroyableWall : BaseMazeSite, IMazeWall
    {
        public bool CanDestroy => false;

        public bool IsDestroyed { get; protected set; }

        public override MoveResult Enter(IMazePlayer player, MoveDirection direction)
        {
            var prev = this[direction.Opposite()] as IMazeRoom;

            return new MoveResult()
            {
                Status = MoveStatus.Failure,
                MazeSite = this.GetType().Name,

                // TODO: Do I really need to return address even if failure?
                Point = new MazePoint(prev.Row, prev.Column)
            };
        }
    }
}
