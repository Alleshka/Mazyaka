using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameService
{
    /// <summary>
    /// Кординаты ячейки в лабиринте
    /// </summary>
    public class Point
    {
        public Point()
        {

        }

        /// <param name="line">Строка</param>
        /// <param name="column">Столбец</param>
        public Point(int line, int column) : this()
        {
            Line = line;
            Column = column;
        }

        /// <summary>
        /// Строка
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Столбец
        /// </summary>
        public int Column { get; set; }
    }
}
