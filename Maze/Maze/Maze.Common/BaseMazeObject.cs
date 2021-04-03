using System;

namespace Maze.Common
{
    public interface IBaseMazeObject
    {
        Guid ObjectID { get; }
    }

    /// <summary>
    /// Базовый класс для всех объектов проекта
    /// </summary>
    public abstract class BaseMazeObject : IBaseMazeObject
    {
        /// <summary>
        /// ID объекта
        /// </summary>
        public Guid ObjectID { get; protected set; }

        public BaseMazeObject(Guid objectID)
        {
            ObjectID = objectID;
        }

        public BaseMazeObject() : this(Guid.NewGuid())
        {

        }
    }
}
