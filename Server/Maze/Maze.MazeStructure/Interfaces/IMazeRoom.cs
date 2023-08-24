using Maze.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure.Interfaces
{
    /// <summary>
    /// Interface for maze room
    /// </summary>
    public interface IMazeRoom : IMazeSite
    {
        public int Line { get; }
        public int Column { get; }

        public IMazeSite GetMazeSite(MoveDirection direction);
        public void SetMazeSite(MoveDirection direction, IMazeSite site);
    }
}
