using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal abstract class BaseMazeConnection : BaseMazeSite, IMazeConnection
    {
        public virtual bool CanDestroy => false;

        public virtual bool IsDestroyed { get; protected set; } = false;

        public IMazeRoom GetMazeSite(MoveDirection direction)
        {
            return this[direction];
        }

        public void SetMazeSite(MoveDirection direction, IMazeRoom site)
        {
            this[direction] = site;
        }

        public IMazeRoom this[MoveDirection direction]
        {
            get
            {
                if (Sides.TryGetValue(direction, out IMazeSite side))
                {
                    return (IMazeRoom)side;
                }
                else
                {
                    return null;
                }
            }
            protected set
            {
                Sides[direction] = value;
            }
        }

        public virtual bool Destroy(MoveDirection direction)
        {
            if (CanDestroy && !IsDestroyed)
            {
                IsDestroyed = true;
                return true;
            }

            return false;
        }

        public override MoveResult Enter(IMazePlayer player, MoveDirection direction)
        {
            var fromRoom = this[direction.Opposite()];
            var toRoom = this[direction];

            fromRoom.RemoveCharacter(player);
            toRoom.AddCharacter(player);

            return toRoom.Enter(player, direction);
        }
    }
}
