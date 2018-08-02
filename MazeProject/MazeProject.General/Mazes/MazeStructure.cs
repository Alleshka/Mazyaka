using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General.Mazes
{
    public class MazeStructure
    {
        public Guid ID { get; private set; }
        public int MazeSize
        {
            get => cells.GetLength(0);
        }

        private MazeCell[][] cells;

        public MazeStructure(int size)
        {
            ID = Guid.NewGuid();
            cells = new MazeCell[size][];
            for (int i = 0; i < size; i++)
            {
                cells[i] = new MazeCell[size];
                for (int j = 0; j < size; j++)
                {
                    cells[i][j] = new MazeCell();
                }
            }
        }

        public MazeCell this [int line, int column]
        {
            get => cells[line][column];
        }
    }
}
