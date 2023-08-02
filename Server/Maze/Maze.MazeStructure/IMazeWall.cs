using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMazeWall : IMazeSite
    {
        public bool CanDestroy { get; }
        public bool IsDestroyed { get; }

        public bool Destroy();
    }
}
