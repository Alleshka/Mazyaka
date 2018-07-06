using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General.Mazes.MazeStructure
{
    public class MazeStruct
    {
        private Cell[][] cells;

        public MazeStruct(int n = 10)
        {
            cells = new Cell[n][];
            for(int i=0; i<n; i++)
            {
                cells[i] = new Cell[n];
                for(int j=0; j<n; j++)
                {
                    cells[i][j] = new Cell();
                }
            }
        }

        public MazeStruct(Cell[][] mazeStruct)
        {
            cells = mazeStruct;
        }


        public Cell this[int line, int column]
        {
            get => cells[line][column];
        }
        public Cell this[PositionInMaze position]
        {
            get => cells[position.Line][position.Column];
        }
        public int Size
        {
            get => cells.GetLength(0);
        }
    }
}
