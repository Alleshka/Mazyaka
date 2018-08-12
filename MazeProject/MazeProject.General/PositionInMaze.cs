using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General
{
    public class PositionInMaze
    { 
        public int Line { get; private set; }
        public int Column { get; private set; }

        public PositionInMaze(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }
    }
}
