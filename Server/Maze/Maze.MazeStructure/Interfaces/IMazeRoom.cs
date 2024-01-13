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
    }
}
