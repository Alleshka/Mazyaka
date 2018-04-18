using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    /// <summary>
    /// Ячейка лабиринта
    /// </summary>
    [DataContract(IsReference = true)]
    public class Cell
    {
        [DataMember]
        private MazePoint address; // Адрес ячейки
        [DataMember]
        private List<BaseGameObject> content; // Содержимое ячейки

        [DataMember]
        private Cell leftCell;
        [DataMember]
        private Cell rightCel;
        [DataMember]
        private Cell upCell;
        [DataMember]
        private Cell downCell;

        public MazePoint Address
        {
            get => address.Clone() as MazePoint;
        }
        public Cell Left
        {
            get
            {
                return leftCell;
            }
            set
            {
                // TODO : Внести проверку на то, что при крайних значениях ссылка либо null, либо ячейка содержит выход
                leftCell = value;
            }
        }
        public Cell Right
        {
            get
            {
                return rightCel;
            }
            set
            {
                // TODO : Внести проверку на то, что при крайних значениях ссылка либо null, либо ячейка содержит выход
                // Иначе кидать исключение
                rightCel = value;
            }
        }
        public Cell Up
        {
            get
            {
                return upCell;
            }
            set
            {
                // TODO : Внести проверку на то, что при крайних значениях ссылка либо null, либо ячейка содержит выход
                upCell = value;
            }
        }
        public Cell Down
        {
            get
            {
                return downCell;
            }
            set
            {
                // TODO : Внести проверку на то, что при крайних значениях ссылка либо null, либо ячейка содержит выход
                downCell = value;
            }
        }

        public Cell(int line, int column)
        {
            InitCell(line, column);
        }
        public Cell(MazePoint cell)
        {
            InitCell(cell.Line, cell.Column);
        }
        public Cell this[MoveDirection direction]
        {
           get
            {
                switch(direction)
                {
                    case MoveDirection.UP: return Up;
                    case MoveDirection.DOWN: return Down;
                    case MoveDirection.RIGHT: return Right;
                    case MoveDirection.LEFT: return Left;
                    default: throw new Exception("Неизвестное набравление");
                }
            }
        }

        public void AddObject(BaseGameObject gameObject)
        {
            content.Add(gameObject);
        }
        public void RemoveObject(BaseGameObject gameObject)
        {
            content.Remove(gameObject);
        }
        public bool ReplaceObject(BaseGameObject gameObject, Cell newCell)
        {
            if (newCell == null) return false;
            else
            {
                newCell.AddObject(gameObject);
                gameObject.CurAddres = newCell.address;

                RemoveObject(gameObject);
                return true;
            }
        }
        public bool ReplaceObject(BaseGameObject gameObject, MoveDirection direction)
        {
            return ReplaceObject(gameObject, this[direction]);
        }

        private void InitCell(int line, int column)
        {
            address = new MazePoint(line, column);
            content = new List<BaseGameObject>();

            Left = null;
            Right = null;
            Up = null;
            Down = null;
        }
    }
}
