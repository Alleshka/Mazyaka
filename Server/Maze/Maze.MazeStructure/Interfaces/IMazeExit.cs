using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeExit : IMazeSite
    {
        public int Line { get; }
        public int Col { get; }
    }
}
