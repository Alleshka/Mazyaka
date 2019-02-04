using MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    /// <summary>
    /// Структура лабиринта
    /// </summary>
    public class MazeStruct
    {
        private Cell[][] _cells;
        private int _size;

        public MazeStruct(int size, IMazeGenerator generator)
        {
            _size = size;
            this._cells = generator.GenerateMaze(size);
        }

        public Cell this[int line, int column] => _cells[line][column];
        public int Size => _size;
    }
}
