﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core
{
    public class Point
    {
        public Point()
        {

        }

        public Point(int line, int column) : this()
        {
            Line = line;
            Column = column;
        }

        public int Line { get; set; }
        public int Column { get; set; }
    }
}
