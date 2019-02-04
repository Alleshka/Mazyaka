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
        protected Dictionary<Type, Action> _executeList { get; } // Список типов и действий при взаимодействии

        /// <summary>
        /// Действие предмета на другой предмет
        /// </summary>
        /// <param name="gameObject"></param>
        public virtual void Exectue(BaseGameObject gameObject)
        {
            if(_executeList.TryGetValue(gameObject.GetType(), out Action action))
            {
                action?.Invoke(); // Выполняем действие
            }
        }
    }
}
