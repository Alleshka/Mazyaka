using MazeSolution.Core.GameObjects;
using MazeSolution.Core.GameObjects.GameObjects;
using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core
{
    public class Maze<T> : BaseMazeObject where T : ICell, new()
    {
        public ICell this[int line, int column] => _mazeStructrure[line, column];

        protected IMazeStructure _mazeStructrure;
        protected Dictionary<Guid, ICell> _mapGameObjects;

        protected Dictionary<Direction, ICell> _mapExitCell;

        private IActionStorage _actionStorage;

        public Maze(IMazeStructure mazeStructrure, IActionStorage actionStorage)
        {
            _actionStorage = actionStorage;
            _mapExitCell = new Dictionary<Direction, ICell>();
            _mapGameObjects = new Dictionary<Guid, ICell>();
            _mazeStructrure = mazeStructrure;
            SetExit(_mazeStructrure);
        }

        private void SetExit(IMazeStructure structure)
        {
            var exitObj = new ExitObject(_actionStorage.EndGameAction);

            Random T = new Random();
            int index = T.Next(0, structure.LineCount);

            // exitCell.AddGameObject(null); Объект выхода
            var relation = new None();

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

        public bool MoveLiveGameObject(Guid liveGameObjectID, Direction direction)
        {
            var oldCell = _mapGameObjects[liveGameObjectID];
            var relation = oldCell.GetRelation(direction);

            relation.Visible = true;
            if (relation?.CanMove == true)
            {
                var newCell = relation.GetNextCell;
                var gameObject = oldCell.RemoveGameObject(liveGameObjectID);
                newCell.AddGameObject(gameObject);
                _mapGameObjects[liveGameObjectID] = newCell;
                ActivateGameObjectsInCell(newCell, gameObject as BaseLiveGameObject);
                return true;
            }
            else return false;
        }

        public void AddLiveGameObject(BaseLiveGameObject gameObject, int line, int column)
        {
            var cell = this._mazeStructrure[line, column];
            cell.AddGameObject(gameObject);
            _mapGameObjects[gameObject.ObjectID] = cell;
        }

        public void ActivateGameObjectsInCell(ICell cell, BaseLiveGameObject activator)
        {
            foreach (var gameObject in cell.GetGameObjects())
            {
                gameObject.Execute(activator);
            }
        }

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
