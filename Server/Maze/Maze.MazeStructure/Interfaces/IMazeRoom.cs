using Maze.Common;
using Maze.Core;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeRoom : IMazeSite
    {
        public MazePoint Address { get; }

        public int Row { get; }
        public int Column { get; }

        public void AddCharacter(IMoveable character);
        public void RemoveCharacter(IMoveable character);

        public IMazeConnection this[MoveDirection direction] { get; }
        public IMazeConnection GetMazeSite(MoveDirection direction);
        public void SetMazeSite(MoveDirection direction, IMazeConnection site);
    }
}
