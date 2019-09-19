using System;

namespace MazeSolution.Core
{
    /// <summary>
    /// Интерфейс базового объекта лабиринта
    /// </summary>
    public interface IBaseMazeObject
    {
        Guid ObjectID { get; }
    }

    /// <summary>
    /// Базовый класс для всех объектов
    /// </summary>
    public class BaseMazeObject : IBaseMazeObject
    {
        public Guid ObjectID { get; set; } = Guid.NewGuid();
    }
}
