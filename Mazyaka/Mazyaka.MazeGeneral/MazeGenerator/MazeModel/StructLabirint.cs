using System.Runtime.Serialization;

namespace Mazyaka.MazeGeneral.MazeModel
{
    /// <summary>
    /// Структура лабиринта
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(Cell))]
    [KnownType(typeof(Point))]
    public class MazeStruct
    {
        /// <summary>
        /// Ячейки
        /// </summary>
        [DataMember]
        private Cell[,] Cells { get; set; }
        [DataMember]
        public int Size { get; private set; }

        public MazeStruct(int sizeLabirint = 10)
        {
            Size = sizeLabirint; 
        }

        public MazeStruct(Cell[,] cells)
        {
            Size = cells.GetLength(0);
            this.Cells = cells;
        }

        public void SetCells(Cell[,] cell)
        {
            Cells = cell;
        }

        public Cell this [int line, int column] => Cells[line, column];
        public Cell this [Point position] => Cells[position.Line, position.Column];
    }
}