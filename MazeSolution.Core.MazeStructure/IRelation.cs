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

        IRelationType RelationType { get; set; }

        bool Visible { get; set; }
    }

    public class BaseRelation : IRelation
    {
        protected readonly ICell _nextCell;

        public BaseRelation(ICell cell)
        {
            _nextCell = cell;
            Visible = false;
        }

        public ICell GetNextCell => _nextCell;

        public IRelationType RelationType { get; set; }

        public bool CanMove => RelationType.CanMove;

        public bool Visible { get; set; }
    }    
}
