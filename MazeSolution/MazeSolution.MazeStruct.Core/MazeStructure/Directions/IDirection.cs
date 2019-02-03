using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public interface IDirection
    {
        /// <summary>
        /// Установить связь с ячейкой
        /// </summary>
        /// <param name="startCell">Стартовя ячейка</param>
        /// <param name="nextCell">Конечная ячейка</param>
        /// <param name="CanMove">Возможность передвижения</param>
        /// <returns></returns>
        bool AddRelation(Cell startCell, Cell nextCell, bool CanMove);

        /// <summary>
        /// Убрать связь с ячейкой
        /// </summary>
        /// <param name="curCell">Текущая ячейка</param>
        bool RemoveRelation(Cell curCell);

        /// <summary>
        /// Установить возможность перехода по связи
        /// </summary>
        /// <param name="curCell">Ячейка</param>
        /// <param name="canMoveStatus">Статус перехода</param>
        void SetMoveStatus(Cell curCell, bool canMoveStatus);

        Cell GetNextCell(Cell curCell);
    }

    public abstract class BaseDirection : IDirection
    {
        protected abstract DirectionEnum _relatedEnum { get; }

        public bool AddRelation(Cell startCell, Cell nextCell, bool CanMove)
        {
            var relation = new Relation(startCell, nextCell, CanMove);
            startCell[_relatedEnum] = relation;
            return true;
        }

        public Cell GetNextCell(Cell curCell)
        {
            return curCell[_relatedEnum]?.NextCell ?? null;
        }

        public bool RemoveRelation(Cell curCell)
        {
            curCell[_relatedEnum] = null;
            return true;
        }

        public void SetMoveStatus(Cell curCell, bool canMoveStatus)
        {
            curCell[_relatedEnum].CanMove = canMoveStatus;
        }
    }
}
