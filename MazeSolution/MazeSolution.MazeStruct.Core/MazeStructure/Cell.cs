using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    public class Cell : BaseMazeObject
    {
        protected int line;
        protected int column;

        public bool Visited { get; set; }

        protected internal Dictionary<Directions.DirectionEnum, Relation> _relations;

        public Relation this[Directions.DirectionEnum direction]
        {
            get
            {
                return _relations[direction];
            }
            protected internal set
            {
                _relations[direction] = value;
            }
        }

        public Cell()
        {
            _relations = new Dictionary<Directions.DirectionEnum, Relation>()
            {
                { Directions.DirectionEnum.Up, null },
                { Directions.DirectionEnum.Down, null },
                {Directions.DirectionEnum.Right, null },
                {Directions.DirectionEnum.Left, null }
            };

            Visited = false;
        }

        public Cell(int line, int column) : this()
        {
            this.line = line;
            this.column = column;
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

        public Cell GetNextCell(Directions.DirectionEnum @enum)
        {
            var direction = Directions.DirectionManager.GetDirection(@enum);
            return direction.GetNextCell(this);
        }
    }
}
