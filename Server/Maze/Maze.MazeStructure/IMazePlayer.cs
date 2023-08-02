using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMazePlayer
    {
        public int Line { get; set; }
        public int Col { get; set; }
    }
}
