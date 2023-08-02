using Maze.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    public interface IMazeGame
    {
        public IMaze CreateMaze(IMazeGenerator generator, IMazeBuilder mazeBuilder);
        public void SetPlayer(int line, int col);
        public MoveResult MovePlayer(MoveDirection direction);
    }
}
