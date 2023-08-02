using Maze.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    /// <summary>
    /// Interface for maze sites (e.g. door, wall, passage e.t.c.)
    /// </summary>
    public interface IMazeSite
    {
        public MoveResult Enter(IMazePlayer player);
    }
}
