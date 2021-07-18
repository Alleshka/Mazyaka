using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure.MazeStructure
{
    class SquareMazeStructure : IMazeStructure
    {
        private int _size;
        private ICell[][] _cells;

        public ICell this[int row, int col] => GetCell(row, col);

        public int RowCount => _size;

        public int ColCount => _size;

        /// <summary>
        /// Если передали только размер
        /// </summary>
        /// <param name="size"></param>
        public SquareMazeStructure(int size)
        {
            _size = size;
            _cells = new ICell[size][];

            // TODO: Настроить дефолтные ячейки
            for (int i = 0; i < size; i++)
            {
                _cells[i] = new ICell[size];
                for (int j = 0; j < size; j++)
                {
                    _cells[i][j] = null;
                }
            }

            // TODO: Запускать генерацию, если передали только размер
        }

        /// <summary>
        /// Передаётся размер и ячейки
        /// </summary>
        /// <param name="size"></param>
        /// <param name="cells"></param>
        public SquareMazeStructure(int size, ICell[][] cells)
        {
            _size = size;
            for(int i = 0; i< size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    _cells[i][j] = cells[i][j];
                }
            }
        }

        public ICell GetCell(int row, int col)
        {
            return _cells[row][col];
        }
    }
}
