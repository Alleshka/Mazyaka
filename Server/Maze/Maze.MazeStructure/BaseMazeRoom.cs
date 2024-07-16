using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal abstract class BaseMazeRoom : BaseMazeSite, IMazeRoom
    {
        public MazePoint Address { get; protected set; }
        public int Row => Address.Row;
        public int Column => Address.Column;

        protected ICollection<IMoveable> _characters;

        public BaseMazeRoom(int row, int col) : this(new MazePoint(row, col))
        {
        }

        public BaseMazeRoom(MazePoint address)
        {
            Address = address;
            _characters = new LinkedList<IMoveable>();
        }

        public override MoveResult Enter(IMazePlayer player, MoveDirection direction)
        {
            return new MoveResult()
            {
                Point = Address,
                Status = MoveStatus.Success,
                MazeSite = this.GetType().Name
            };
        }

        public void AddCharacter(IMoveable character)
        {
            _characters.Add(character);
        }

        public void RemoveCharacter(IMoveable character)
        {
            _characters.Remove(character);
        }

        public IMazeConnection GetMazeSite(MoveDirection direction)
        {
            return this[direction];
        }

        public void SetMazeSite(MoveDirection direction, IMazeConnection site)
        {
            this[direction] = site;
        }

        public IMazeConnection this[MoveDirection direction]
        {
            get
            {
                if (Sides.TryGetValue(direction, out IMazeSite side))
                {
                    return (IMazeConnection)side;
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
    }
}
