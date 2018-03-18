using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mazyaka.MazeGeneral.MazeModel
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Point))]
    [KnownType(typeof(Exit))]
    [KnownType(typeof(FalseKey))]
    [KnownType(typeof(Key))]
    [KnownType(typeof(Human))]
    [KnownType(typeof(Minotaur))]
    /// <summary>
    /// Ячейка лабиринта    
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Адрес ячейки
        /// </summary>
        [DataMember]
        public Point Address { get; private set; }

        /// <summary>
        /// Объекты в ячейке
        /// </summary>
        [DataMember]
        public List<GameObject> Content { get; private set; }

        /// <summary>
        /// Ссылки на соседние ячейки. 
        /// Если стена - null
        /// </summary>
        [DataMember]
        public Cell UP { get; set; }
        [DataMember]
        public Cell LEFT { get; set; }
        [DataMember]
        public Cell RIGHT { get; set; }
        [DataMember]
        public Cell DOWN { get; set; }

        public Cell(int line, int column)
        {
            Init(line, column);
        }
        public Cell(Point point)
        {
            Init(point.Line, point.Column);
        }

        private void Init(int line, int column)
        {
            Address = new Point(line, column);
            Content = new List<GameObject>();

            UP = null;
            LEFT = null;
            RIGHT = null;
            DOWN = null;
        }

        public void Add(GameObject obj)
        {
            Content.Add(obj);
        }

        /// <summary>
        /// Convert Cell to Flags format
        /// </summary>
        /// <param name = "cell" > cell to convert</param>
        //public static implicit operator Flags(Cell cell)
        //{
        //    Flags cellFlags = 0;
        //    if (cell.LEFT == null) cellFlags |= Flags.WALL_LEFT;
        //    if (cell.RIGHT == null) cellFlags |= Flags.WALL_RIGHT;
        //    if (cell.DOWN == null) cellFlags |= Flags.WALL_DOWN;
        //    if (cell.UP == null) cellFlags |= Flags.WALL_UP;

        //    return cellFlags;
        //}

        /// <summary>
        /// Convert Flags to Cell format
        /// </summary>
        /// <param name = "cellFlags" > cell in Flags format</param>
        /// <param name = "line" > cell line number</param>
        /// <param name = "column" > cell column number</param>
        /// <returns>Converted Cell</returns>
        // public static Cell FlagsToCell(Flags cellFlags, int line, int column)
        //{
        //    Cell cell = new Cell(line, column);
        //    if ((cellFlags & Flags.WALL_LEFT) == 0) cell.LEFT = new Cell(line, column - 1);
        //    if ((cellFlags & Flags.WALL_RIGHT) == 0) cell.RIGHT = new Cell(line, column + 1);
        //    if ((cellFlags & Flags.WALL_UP) == 0) cell.UP = new Cell(line - 1, column);
        //    if ((cellFlags & Flags.WALL_DOWN) == 0) cell.DOWN = new Cell(line + 1, column);
        //    return cell;
        //}
    }   
}