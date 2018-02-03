using System;

namespace Mazyaka.Server.Model.MazeModel
{
    /// <summary>
    /// Координаты в лабиринте
    /// </summary>
    public class Point
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Point(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }
}
