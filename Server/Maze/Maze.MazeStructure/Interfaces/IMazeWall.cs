using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeWall : IMazeSite
    {
        public bool CanDestroy { get; }
        public bool IsDestroyed { get; }
    }
}
