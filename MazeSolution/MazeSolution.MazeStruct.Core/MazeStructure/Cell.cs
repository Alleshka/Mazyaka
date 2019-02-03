using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    public class Cell
    {
        public Relation UpRelation { get; protected internal set; }
        public Relation RightRelation { get; protected internal set; }
        public Relation DownRelation { get; protected internal set; }
        public Relation LeftRelation { get; protected internal set; }

        public Cell()
        {
            UpRelation = null;
            RightRelation = null;
            DownRelation = null;
            LeftRelation = null;
        }

        /// <summary>
        /// Установить отношение с текущей ячейкй
        /// </summary>
        /// <param name="nextCell">Связываемая ячейка</param>
        /// <param name="enum">Направление</param>
        public bool AddRelation(Cell nextCell, Directions.DirectionEnum @enum, bool canMove)
        {
            var direction = Directions.DirectionManager.GetDirection(@enum);
            direction?.AddRelation(this, nextCell, canMove);
            return direction != null;
        }

        /// <summary>
        /// Убрать связь с ячейкой
        /// </summary>
        /// <param name="enum">Направление</param>
        public void RemoveRelation(Directions.DirectionEnum @enum)
        {
            var direction = Directions.DirectionManager.GetDirection(@enum);
            direction?.RemoveRelation(this);
        }

        /// <summary>
        /// Удалить стену в отношении
        /// </summary>
        /// <param name="enum"></param>
        public void RemoveWall(Directions.DirectionEnum @enum)
        {
            var direction = Directions.DirectionManager.GetDirection(@enum);
            direction?.SetMoveStatus(this, true);
        }

        /// <summary>
        /// Добавить стену
        /// </summary>
        /// <param name="enum"></param>
        public void AddWall(Directions.DirectionEnum @enum)
        {
            var direction = Directions.DirectionManager.GetDirection(@enum);
            direction?.SetMoveStatus(this, false);
        }
    }
}
