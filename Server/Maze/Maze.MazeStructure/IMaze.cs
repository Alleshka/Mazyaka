using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMaze
    {
        public void AddRoom(IMazeRoom room);
        public IMazeRoom GetRoomByCoordinates(int line, int col);
    }
}
