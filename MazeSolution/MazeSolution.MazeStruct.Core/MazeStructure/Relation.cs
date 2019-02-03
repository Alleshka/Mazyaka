using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    /// <summary>
    /// Отношение между ячейками
    /// </summary>
    public class Relation
    {
        /// <summary>
        /// Возможен ли переход по этой связи
        /// </summary>
        public bool CanMove { get; protected internal set; }

        /// <summary>
        /// Начальная ячейка
        /// </summary>
        public Cell StartCell { get; protected internal set; }

        /// <summary>
        /// Ячейка, в которую ведёт данная связь
        /// </summary>
        public Cell NextCell { get; protected internal set; }

        public Relation(Cell startCell, Cell nextCell, bool canMove)
        {
            this.StartCell = startCell;
            this.NextCell = nextCell;
            this.CanMove = canMove;
        }

        public Relation(Cell startCell, Cell nextCell) : this (startCell, nextCell, false)
        {

        }

        public Relation(Cell startCell) : this (startCell, null, false)
        {

        }
    }
}
