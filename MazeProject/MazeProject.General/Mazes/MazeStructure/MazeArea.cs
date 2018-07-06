using System;
using System.Collections.Generic;
using System.Text;
using MazeProject.General.Mazes.MazeGenerator;

namespace MazeProject.General.Mazes.MazeStructure
{
    public enum MoveDirection { Up, Right, Down, Left}
    public class MazeArea
    {
        public Guid MazeID { get; private set; }

        private MazeStruct mazeStruct;
        private Cell ExitCell;

        public MazeArea()
        {
            MazeID = Guid.NewGuid();
            ExitCell = new Cell();
        }

        public void Generate(IMazeGenerator mazeGenerator, int size)
        {
            mazeStruct = mazeGenerator.Generate(size);
        }

        public bool MoveObject(Guid objectID, MoveDirection direction)
        {
            // TODO: Возможно, стоит хранить координаты всех объектов и изменять их
            Cell curCell = FindCellWithObject(objectID);
            Cell newCell = null;

            if (GetNewCell(curCell, direction) is Cell) newCell = GetNewCell(curCell, direction) as Cell;

            if (newCell == null) return false;
            else
            {
                newCell.AddObject(curCell.GetObject(objectID));
                curCell.RemoveObject(objectID);
                return true;
            }
        }

        private Cell FindCellWithObject(Guid objectID)
        {
            for(int i=0; i<mazeStruct.Size; i++)
            {
                for(int j=0; j<mazeStruct.Size; j++)
                {
                    if (mazeStruct[i, j].IsContainObject(objectID)) return mazeStruct[i, j];
                }
            }

            return null;
        }
        private MazeBlock GetNewCell(Cell oldCell, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Down: return oldCell.Down;
                case MoveDirection.Left: return oldCell.Left;
                case MoveDirection.Right: return oldCell.Right;
                case MoveDirection.Up: return oldCell.Up;
                default: return null;
            }
        }
    }
}
