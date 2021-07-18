using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure
{
    /// <summary>
    /// Интерфейс структуры лабиринта
    /// Нужен для работы со связями
    /// </summary>
    interface IMazeStructure
    {
        /// <summary>
        /// В любой структуре должна быть возможность обращения по индексу
        /// </summary>
        /// <param name="row">Номер строки</param>
        /// <param name="col">Номер колонки</param>
        /// <returns></returns>
        ICell GetCell(int row, int col);
        ICell this[int row, int col] { get; }

        int RowCount { get; }
        int ColCount { get; }
    }
}
