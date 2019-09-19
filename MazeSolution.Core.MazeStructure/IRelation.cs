using MazeSolution.Core.GameObjects;
using System;

namespace MazeSolution.Core.MazeStructrure
{
    /// <summary>
    /// Описание связи между ячейками
    /// </summary>
    public interface IRelation
    {
        /// <summary>
        /// Доступность прохода по этой связи
        /// </summary>
        bool CanMove { get; }

        /// <summary>
        /// Следующая ячейка
        /// </summary>
        ICell GetNextCell { get; }

        /// <summary>
        /// Тип связи
        /// </summary>
        IRelationType RelationType { get; set; }

        /// <summary>
        /// Видимость связи
        /// </summary>
        bool Visible { get; set; }
    }

    /// <summary>
    /// Базовая реализация связи
    /// </summary>
    public class BaseRelation : IRelation
    {
        protected readonly ICell _nextCell;

        /// <param name="cell">Ячейка</param>
        public BaseRelation(ICell cell)
        {
            _nextCell = cell;
            Visible = false;
        }

        /// <summary>
        /// Получает связанную ячейку
        /// </summary>
        public ICell GetNextCell => _nextCell;

        /// <summary>
        /// Тип связи
        /// </summary>
        public IRelationType RelationType { get; set; }

        // TODO: А что будет если можно прояти только в одну сторону, но не в противоположную?
        /// <summary>
        /// Признак указывающий можно ли перейти от одной связываемой ячейки к другой
        /// </summary>
        public bool CanMove => RelationType.CanMove;

        /// <summary>
        /// Видимость связи
        /// </summary>
        public bool Visible { get; set; }
    }    
}
