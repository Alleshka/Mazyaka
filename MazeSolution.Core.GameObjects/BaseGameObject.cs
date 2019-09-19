using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameObjects
{
    /// <summary>
    /// Базовый игровой объект
    /// </summary>
    public abstract class BaseGameObject : BaseMazeObject
    {
        /// <summary>
        /// Активация объекта
        /// </summary>
        /// <param name="liveGameObject">Объект-активатор</param>
        public abstract void Execute(BaseLiveGameObject liveGameObject);
    }

    /// <summary>
    /// TODO: Метод EXECUTE проверяет наличие резиста у живого объекта, если резиста нет - срабатывает
    /// </summary>

    public abstract class BaseLiveGameObject : BaseGameObject
    {
        public double HealthPoints { get; set; }
        public double ActionPoints { get; set; }
        // TODO: Список эффектов
        // TODO: Инвентарь
    }
}
