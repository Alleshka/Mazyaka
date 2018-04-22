using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    /// <summary>
    /// Структура лабиринта
    /// </summary>
    [DataContract]
    public class MazeStruct
    {
        [DataMember]
        private Cell[][] cells;

        public MazeStruct(int mazeSize)
        {
            cells = new Cell[mazeSize][];

            for(int i = 0; i < mazeSize; i++)
            {
                cells[i] = new Cell[mazeSize];
                for(int j = 0; j<mazeSize; j++)
                {
                    cells[i][j] = new Cell(i, j);
                }
            }
        }

        private MazeStruct()
        {

        }

        public MazeStruct(Cell[][] @struct)
        {
            cells = @struct.ToArray();
        }
        public MazeStruct(MazeStruct @struct)
        {
            cells = new Cell[@struct.Size][];
            for(int i=0; i<@struct.Size; i++)
            {
                cells[i] = new Cell[@struct.Size];
                for(int j=0; j<@struct.Size; j++)
                {
                    cells[i][j] = @struct[i, j];
                }
            }
        }

        public Cell this[int line, int column] => cells[line][column];
        public Cell this[MazePoint point] => cells[point.Line][point.Column];
        public int Size => cells.GetLength(0);
    }
}
