using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.MazeStruct.Core.GameObjects;
using MazeSolution.MazeStruct.Core.MazeStructure.Directions;
using System.Linq;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    public abstract class BaseCell : BaseMazeObject
    {
        protected int line;
        protected int column;

        public bool Visited { get; set; }

        protected internal abstract Dictionary<DirectionEnum, Relation> Relations { get; }
        protected List<BaseGameObject> _objectsList;

        public Relation this[DirectionEnum direction]
        {
            get
            {
                return Relations[direction];
            }
            protected internal set
            {
                Relations[direction] = value;
            }
        }

        public BaseCell()
        {
            _objectsList = new List<BaseGameObject>();
            Visited = false;
        }

        public BaseCell(int line, int column) : this()
        {
            this.line = line;
            this.column = column;
        }

        /// <summary>
        /// Установить отношение с текущей ячейкй
        /// </summary>
        /// <param name="nextCell">Связываемая ячейка</param>
        /// <param name="enum">Направление</param>
        public bool AddRelation(BaseCell nextCell, DirectionEnum @enum, bool canMove)
        {
            var relation = new Relation(this, nextCell, canMove);
            this[@enum] = relation;
            return true;
        }

        /// <summary>
        /// Убрать связь с ячейкой
        /// </summary>
        /// <param name="enum">Направление</param>
        public void RemoveRelation(DirectionEnum @enum)
        {
            this[@enum] = null;
        }

        /// <summary>
        /// Удалить стену в отношении
        /// </summary>
        /// <param name="enum"></param>
        public void RemoveWall(DirectionEnum @enum)
        {
            this[@enum].CanMove = true;
        }

        /// <summary>
        /// Добавить стену
        /// </summary>
        /// <param name="enum"></param>
        public void AddWall(DirectionEnum @enum)
        {
            this[@enum].CanMove = false;
        }

        public BaseCell GetNextCell(DirectionEnum @enum)
        {
            return this[@enum]?.NextCell ?? null;
        }

        public bool AddGameObject(BaseGameObject obj)
        {
            this._objectsList.Add(obj);
            return true;
        }

        public void RemoveObject(BaseGameObject obj)
        {
            this._objectsList.Remove(obj);
        }

        public IEnumerable<Type> TypesInCell()
        {
            return _objectsList.Select(x => x.GetType());
        }
    }

    public class SquareCell : BaseCell
    {
        Dictionary<DirectionEnum, Relation> _relations;
        public SquareCell() : base()
        {
            _relations = new Dictionary<DirectionEnum, Relation>()
            {
                { DirectionEnum.Up, null },
                { DirectionEnum.Down, null },
                { DirectionEnum.Right, null },
                { DirectionEnum.Left, null }
            };
        }

        public SquareCell(int line, int column) : this ()
        {
            this.line = line;
            this.column = column;
        }

        protected internal override Dictionary<DirectionEnum, Relation> Relations => _relations;
    }
}
