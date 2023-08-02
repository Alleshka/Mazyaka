using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMazeBuilder
    {
        public void BuildMaze();
        public void BuildRoom(int line, int col);
        public void BuildPassage(int line1, int col1, int line2, int col2);
        public void BuildPassage(IMazeRoom room1, IMazeRoom room2);

        public IMaze GetMaze();
    }
}
