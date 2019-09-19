using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MazeSolution.Core.GameObjects;

namespace MazeSolution.Core.MazeStructrure
{
    /// <summary>
    /// Направление
    /// </summary>
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
        /// <returns>Связь</returns>
        IRelation GetRelation(Direction direction);

        IRelation this[Direction direction] { get; }

        /// <summary>
        /// Добавить свзяь
        /// </summary>
        /// <param name="direction">Направление связи</param>
        /// <param name="type">Тип связи</param>
        /// <param name="cell">Связываемая ячейка</param>
        void AddRelation(Direction direction, IRelationType type, ICell cell);

        /// <summary>
        /// Признак посещенности ячейки
        /// </summary>
        bool Visited { get; set; }

        /// <summary>
        /// Справочник связей ячейки
        /// </summary>
        Dictionary<Direction, IRelation> AllRelations { get; }

        /// <summary>
        /// Добавить игровой объект в ячейку
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        void AddGameObject(BaseGameObject gameObject);

        /// <summary>
        /// Получить игровой объект ячейки
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Игровой объект</returns>
        BaseGameObject GetGameObject<T>();

        /// <summary>
        /// Удалить игровой объект из ячейки
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        /// <returns>Удаленный игровой объект</returns>
        BaseGameObject RemoveGameObject(BaseGameObject gameObject);

        /// <summary>
        /// Удалить игровой объект из ячейки по его ID
        /// </summary>
        /// <param name="objectID">ID игрового объекта</param>
        /// <returns>Игровой объект</returns>
        BaseGameObject RemoveGameObject(Guid objectID);

        /// <summary>
        /// Получить перечисление игровых объектов ячейки
        /// </summary>
        /// <returns>Перечисление игровых объектов ячейки</returns>
        IEnumerable<BaseGameObject> GetGameObjects();

        /// <summary>
        /// Установить статус видимости связей
        /// </summary>
        /// <param name="status">Статус видимости</param>
        void SetRelationVisibleStatus(bool status);
    }

    /// <summary>
    /// Базовый класс ячейки
    /// </summary>
    public abstract class BaseCell : BaseMazeObject, ICell
    {
        /// <summary>
        /// Игровые объекты в ячейке
        /// </summary>
        protected HashSet<BaseGameObject> _gameObjects;

        /// <summary>
        /// Справочник связей ячейки
        /// </summary>
        protected Dictionary<Direction, IRelation> RelationMap { get; }

        /// <summary>
        /// Признак посещенности ячейки
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Справочник связей ячейки
        /// </summary>
        public Dictionary<Direction, IRelation> AllRelations => new Dictionary<Direction, IRelation>(RelationMap);

        public IRelation this[Direction direction] => GetRelation(direction);

        protected BaseCell()
        {
            RelationMap = new Dictionary<Direction, IRelation>();
            _gameObjects = new HashSet<BaseGameObject>();
        }

        /// <summary>
        /// Получить связь с ячейкой
        /// </summary>
        /// <param name="direction">Направление</param>
        /// <returns>Связь</returns>
        public IRelation GetRelation(Direction direction)
        {
            RelationMap.TryGetValue(direction, out IRelation relation);
            return relation;
        }

        /// <summary>
        /// Добавить свзяь
        /// </summary>
        /// <param name="direction">Направление связи</param>
        /// <param name="type">Тип связи</param>
        /// <param name="cell">Связываемая ячейка</param>
        public void AddRelation(Direction direction, IRelationType type, ICell cell)
        {
            RelationMap[direction] = new BaseRelation(cell) { RelationType = type };        
        }

        /// <summary>
        /// Добавить игровой объект в ячейку
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        public void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        /// <summary>
        /// Удалить игровой объект из ячейки
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        /// <returns>Удаленный игровой объект</returns>
        public BaseGameObject RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
            return gameObject;
        }

        /// <summary>
        /// Удалить игровой объект из ячейки по его ID
        /// </summary>
        /// <param name="objectID">ID игрового объекта</param>
        /// <returns>Игровой объект</returns>
        public BaseGameObject RemoveGameObject(Guid objectID)
        {
            var gameObject = _gameObjects.FirstOrDefault(x => x.ObjectID == objectID);
            return RemoveGameObject(gameObject);
        }

        /// <summary>
        /// Получить игровой объект ячейки
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Игровой объект</returns>
        public BaseGameObject GetGameObject<T>()
        {
            return _gameObjects.FirstOrDefault(x => x is T);
        }

        /// <summary>
        /// Получить перечисление игровых объектов ячейки
        /// </summary>
        /// <returns>Перечисление игровых объектов ячейки</returns>
        public IEnumerable<BaseGameObject> GetGameObjects() => _gameObjects;

        /// <summary>
        /// Установить статус видимости связей
        /// </summary>
        /// <param name="status">Статус видимости</param>
        public void SetRelationVisibleStatus(bool status)
        {
            foreach(var rel in AllRelations.Values)
            {
                rel.Visible = status;
            }
        }
    }
}
