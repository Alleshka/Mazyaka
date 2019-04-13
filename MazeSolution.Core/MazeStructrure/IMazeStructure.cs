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


    public class SquareMaze <T> : BaseMazeObject, IMazeStructure where T : BaseCell, new()
    {
        protected ICell[][] _cells;

        public SquareMaze(int size) : this(size, size)
        {

        }

        public SquareMaze(int lineCount, int columnCount)
        {
            _cells = new T[lineCount][];
            for(int i=0; i<lineCount; i++)
            {
                _cells[i] = new SquareCell[columnCount];
                for(int j=0; j<columnCount; j++)
                {
                    _cells[i][j] = new T();
                }
            }
        }

        public ICell this[int line, int column] => _cells[line][column];

        public int LineCount { get; set; }
        public int ColumnCount { get; set; }
    }
}
