using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.MazeStruct.Core.MazeStructure;
using MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators;

namespace MazeSolution.MazeStruct.Core
{
    /// <summary>
    /// Класс лабиринта
    /// </summary>
    public abstract class BaseMaze : BaseMazeObject
    {
        protected MazeStructureClass _structure; // Структура лабиринта

        protected abstract IMazeGenerator _generator { get; } // Генератор
        protected abstract int _mazeSize { get; } // Размер

        public BaseMaze()
        {
            _structure = new MazeStructureClass(_mazeSize, _generator); // Генерим структуру лабиринта
        }
    }
}
