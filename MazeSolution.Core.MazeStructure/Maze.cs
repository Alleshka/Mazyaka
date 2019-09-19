using MazeSolution.Core.GameObjects;
using MazeSolution.Core.GameObjects.GameObjects;
using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.MazeStructrure
{
    /// <summary>
    /// Лабиринт
    /// </summary>
    /// <typeparam name="T">Тип ячейки</typeparam>
    public class Maze<T> : BaseMazeObject where T : ICell, new()
    {
        public ICell this[int line, int column] => _mazeStructrure[line, column];

        /// <summary>
        /// Структура лабиринта
        /// </summary>
        protected IMazeStructure _mazeStructrure;

        /// <summary>
        /// Справочник игровых объектов в ячейках
        /// </summary>
        protected Dictionary<Guid, ICell> _mapGameObjects;

        /// <summary>
        /// Справочник выходов из лабиринта
        /// </summary>
        protected Dictionary<Direction, ICell> _mapExitCell;

        /// <summary>
        /// Хранилище операций
        /// </summary>
        private IActionStorage _actionStorage;

        /// <param name="mazeStructrure">Структура лабиринта</param>
        /// <param name="actionStorage">Хранилище операций</param>
        public Maze(IMazeStructure mazeStructrure, IActionStorage actionStorage)
        {
            _actionStorage = actionStorage;
            _mapExitCell = new Dictionary<Direction, ICell>();
            _mapGameObjects = new Dictionary<Guid, ICell>();
            _mazeStructrure = mazeStructrure;
            SetExit(_mazeStructrure);
        }

        /// <summary>
        /// Установить выход из лабиринта
        /// </summary>
        /// <param name="structure">Структура лабиринта</param>
        private void SetExit(IMazeStructure structure)
        {
            var exitObj = new ExitObject(_actionStorage.EndGameAction);

            Random T = new Random();
            int index = T.Next(0, structure.LineCount);

            // exitCell.AddGameObject(null); Объект выхода
            var relation = new Passage();

            ICell exitCell;
            exitCell = new T();
            exitCell.AddGameObject(exitObj);
            structure[0, index].AddRelation(Direction.Up, relation, _mapExitCell[Direction.Up] = exitCell);
            exitCell.AddRelation(Direction.Down, relation, structure[0, index]);

            exitCell = new T();
            exitCell.AddGameObject(exitObj);
            structure[index, 0].AddRelation(Direction.Left, relation, _mapExitCell[Direction.Left] = exitCell);
            exitCell.AddRelation(Direction.Right, relation, structure[index, 0]);

            exitCell = new T();
            exitCell.AddGameObject(exitObj);
            structure[index, structure.ColumnCount - 1].AddRelation(Direction.Right, relation, _mapExitCell[Direction.Right] = exitCell);
            exitCell.AddRelation(Direction.Left, relation, structure[index, structure.ColumnCount - 1]);

            exitCell = new T();
            exitCell.AddGameObject(exitObj);
            structure[structure.LineCount - 1, index].AddRelation(Direction.Down, relation, _mapExitCell[Direction.Down] = exitCell);
            exitCell.AddRelation(Direction.Up, relation, structure[structure.LineCount - 1, index]);
        }

        /// <summary>
        /// Подвинуть живой игровой объект
        /// </summary>
        /// <param name="liveGameObjectID">Живой игровой объект, который будет подвинут</param>
        /// <param name="direction">Направление движения</param>
        /// <returns>True, при успешном выполнении операции, иначе false</returns>
        public bool MoveLiveGameObject(Guid liveGameObjectID, Direction direction)
        {
            var oldCell = _mapGameObjects[liveGameObjectID];
            var relation = oldCell.GetRelation(direction);

            if (relation != null)
            {
                relation.Visible = true;
                if (relation.CanMove == true)
                {
                    var newCell = relation.GetNextCell;
                    var gameObject = oldCell.RemoveGameObject(liveGameObjectID);
                    newCell.AddGameObject(gameObject);
                    _mapGameObjects[liveGameObjectID] = newCell;
                    ActivateGameObjectsInCell(newCell, gameObject as BaseLiveGameObject);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Добавить живой игровой объект в ячейку
        /// </summary>
        /// <param name="gameObject">Живой игровой объект</param>
        /// <param name="line">Номер строки ячейки</param>
        /// <param name="column">Номер столбца ячейки</param>
        public void AddLiveGameObject(BaseLiveGameObject gameObject, int line, int column)
        {
            var cell = this._mazeStructrure[line, column];
            cell.AddGameObject(gameObject);
            _mapGameObjects[gameObject.ObjectID] = cell;
        }

        /// <summary>
        /// Активировать игровые объекты в ячейке
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <param name="activator">Объект-активатор</param>
        public void ActivateGameObjectsInCell(ICell cell, BaseLiveGameObject activator)
        {
            foreach (var gameObject in cell.GetGameObjects())
            {
                gameObject.Execute(activator);
            }
        }

        /// <summary>
        /// Установить видимость ячеек вокруг живого игрового объекта
        /// </summary>
        /// <param name="visible">Видимость</param>
        /// <param name="gameObject">Живой игровой объект</param>
        /// <param name="size">Радиус видимости</param>
        public void SetCellsVisible(bool visible, BaseLiveGameObject gameObject, int size)
        {
            for(int i = 0; i < _mazeStructrure.LineCount; i++)
            {
                for(int j=0; j<_mazeStructrure.ColumnCount; j++)
                {
                    this[i, j].SetRelationVisibleStatus(visible);
                }
            }
        }
    }
}
