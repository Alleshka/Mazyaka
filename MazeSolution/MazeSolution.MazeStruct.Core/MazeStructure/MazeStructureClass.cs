using MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    /// <summary>
    /// Структура лабиринта
    /// </summary>
    public class MazeStructureClass
    {
        private BaseCell[][] _cells;
        private int _size;

        public MazeStructureClass(int size, IMazeGenerator generator)
        {
            _size = size;
            this._cells = generator.GenerateMaze(size);
        }

        public BaseCell this[int line, int column] => _cells[line][column];
        public int Size => _size;
    }
}
