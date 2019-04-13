using System;

namespace MazeSolution.Core
{
    /// <summary>
    /// Базовый класс для всех объектов
    /// </summary>
    public class BaseMazeObject
    {
        public Guid ObjectID { get; set; } = Guid.NewGuid();
    }
}
