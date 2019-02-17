using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.GameObjects
{

    /// <summary>
    /// Базовый класс игрового объека
    /// </summary>
    public abstract class BaseGameObject : BaseMazeObject
    {
        public delegate void BaseMazeAction<in T>(T obj);

        protected Dictionary<Type, BaseMazeAction<BaseGameObject>> _executeList { get; } // Список типов и действий при взаимодействии

        /// <summary>
        /// Действие предмета на другой предмет
        /// </summary>
        /// <param name="gameObject"></param>
        public virtual void Exectue(BaseGameObject gameObject)
        {
            if(_executeList.TryGetValue(gameObject.GetType(), out BaseMazeAction<BaseGameObject> action))
            {
                action?.Invoke(gameObject); // Выполняем действие
            }
        }

    }

    public abstract class LiveGameObject : BaseGameObject
    {

    }
}
