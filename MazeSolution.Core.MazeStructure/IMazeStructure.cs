using MazeSolution.Core.MazeStructrure.Cells;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.MazeStructrure
{
    /// <summary>
    /// Описатель структуры лабиринта
    /// </summary>
    public interface IMazeStructure
    {
        int LineCount { get; set; }
        int ColumnCount { get; set; }
        ICell this[int line, int column] { get; }
    }

    /// <summary>
    /// Квадратный лабиринт
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SquareMaze<T> : BaseMazeObject, IMazeStructure where T : BaseCell, new()
    {
        protected ICell[][] _cells;

        /// <param name="size">Размер лабиринта</param>
        public SquareMaze(int size) : this(size, size)
        {

        }

        /// <param name="lineCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        public SquareMaze(int lineCount, int columnCount)
        {
            _cells = new T[lineCount][];
            for (int i = 0; i < lineCount; i++)
            {
                _cells[i] = new SquareCell[columnCount];
                for (int j = 0; j < columnCount; j++)
                {
                    _cells[i][j] = new T();
                }
            }
        }

        public ICell this[int line, int column] => _cells[line][column];

        /// <summary>
        /// Число строк
        /// </summary>
        public int LineCount { get; set; }

        /// <summary>
        /// Число столбцов
        /// </summary>
        public int ColumnCount { get; set; }
    }
}
