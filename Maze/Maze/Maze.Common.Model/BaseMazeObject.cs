using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Common.Model
{
    /// <summary>
    /// Базовый класс для всех объектов проекта
    /// </summary>
    public abstract class BaseMazeObject
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
