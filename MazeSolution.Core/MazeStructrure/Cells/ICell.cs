using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MazeSolution.Core.GameObjects;

namespace MazeSolution.Core.MazeStructrure
{
    [Flags]
    public enum Direction
    {
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }

    /// <summary>
    /// Интерфейс ячейки
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Получить связь с ячейкой
        /// </summary>
        /// <param name="direction">Направление</param>
        /// <returns></returns>
        IRelation GetRelation(Direction direction);

        IRelation this[Direction direction] { get; }

        void AddRelation(Direction direction, IRelationType type, ICell cell);

        bool Visited { get; set; }

        Dictionary<Direction, IRelation> AllRelations { get; }

        void AddGameObject(BaseGameObject gameObject);
        BaseGameObject GetGameObject<T>();
        BaseGameObject RemoveGameObject(BaseGameObject gameObject);
        BaseGameObject RemoveGameObject(Guid objectID);
    }

    public abstract class BaseCell : BaseMazeObject, ICell
    {
        protected HashSet<BaseGameObject> _gameObjects;

        protected Dictionary<Direction, IRelation> RelationMap { get; }
        public bool Visited { get; set; }

        public Dictionary<Direction, IRelation> AllRelations => new Dictionary<Direction, IRelation>(RelationMap);

        public IRelation this[Direction direction] => GetRelation(direction);

        protected BaseCell()
        {
            RelationMap = new Dictionary<Direction, IRelation>();
            _gameObjects = new HashSet<BaseGameObject>();
        }

        public IRelation GetRelation(Direction direction)
        {
            RelationMap.TryGetValue(direction, out IRelation relation);
            return relation;
        }

        public void AddRelation(Direction direction, IRelationType type, ICell cell)
        {
            RelationMap[direction] = new BaseRelation(cell) { RelationType = type };        
        }

        public void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public BaseGameObject RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
            return gameObject;
        }

        public BaseGameObject RemoveGameObject(Guid objectID)
        {
            var gameObject = _gameObjects.FirstOrDefault(x => x.ObjectID == objectID);
            return RemoveGameObject(gameObject);
        }

        public BaseGameObject GetGameObject<T>()
        {
            return _gameObjects.FirstOrDefault(x => x is T);
        }
    }
}
