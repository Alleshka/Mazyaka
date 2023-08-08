using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMaze
    {
        public int LineCount { get; }
        public int ColCount { get; }

        public void AddRoom(IMazeRoom room);
        public IMazeRoom GetRoomByCoordinates(int line, int col);
    }
}
