using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMazeGenerator
    {
        public IMaze GenerateMaze(IMazeBuilder builder);
    }
}
