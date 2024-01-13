using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal abstract class BaseMazeSite : IMazeSite
    {
        private Dictionary<MoveDirection, IMazeSite> _sides;

        public IMazeSite this[MoveDirection direction]
        {
            get
            {
                if (_sides.TryGetValue(direction, out IMazeSite side))
                {
                    return side;
                }
                else
                {
                    return null;
                }
            }
            protected set
            {
                _sides[direction] = value;
            }
        }

        public BaseMazeSite()
        {
            _sides = new Dictionary<MoveDirection, IMazeSite>();
        }

        public abstract MoveResult Enter(IMazePlayer player, MoveDirection direction);

        public IMazeSite GetMazeSite(MoveDirection direction)
        {
            return this[direction];
        }

        public void SetMazeSite(MoveDirection direction, IMazeSite site)
        {
            this[direction] = site;
        }
    }
}
