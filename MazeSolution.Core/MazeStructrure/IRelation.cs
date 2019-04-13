using MazeSolution.Core.GameObjects;

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
    }

    public class BaseRelation : IRelation
    {
        protected readonly ICell _nextCell;

        public BaseRelation(ICell cell)
        {
            _nextCell = cell;
        }

        public ICell GetNextCell => _nextCell;

        public IRelationType RelationType { get; set; }

        public bool CanMove => RelationType.CanMove;
    }    
}
