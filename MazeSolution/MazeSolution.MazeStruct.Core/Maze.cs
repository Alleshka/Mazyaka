using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.MazeStruct.Core.GameObjects.LiveGameObjects;
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

        internal BaseMaze()
        {

        }

        public virtual void GenerateMaze()
        {
            _structure = new MazeStructureClass(_mazeSize, _generator); // Генерим структуру лабиринта
            SetGameObjects();
        }

        /// <summary>
        /// Проставляет требуемые объекты в лабирин
        /// </summary>
        protected virtual void SetGameObjects()
        {

        }
    }

    public class TestMaze : BaseMaze
    {
        protected override IMazeGenerator _generator => new RecursiveMazeGenerator();

        protected override int _mazeSize => _size;
        private int _size;

        private HumanGameObject _human;
        private int _line;
        private int _column;
        public TestMaze(int size, int startLine, int startColumn)
        {
            _size = size;
            _human = new HumanGameObject();
            _line = startLine;
            _column = startColumn;
        }

        protected override void SetGameObjects()
        {
            this._structure[_line, _column].AddGameObject(_human);
        }

        public BaseCell this[int line, int column] => _structure[line, column];
    }
}
