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
    }
}
