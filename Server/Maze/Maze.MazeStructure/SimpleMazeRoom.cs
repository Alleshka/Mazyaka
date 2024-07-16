using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal class SimpleMazeRoom : BaseMazeRoom, IMazeRoom
    {
        public SimpleMazeRoom(int row, int col) : base(row, col)
        {
        }
    }
}
