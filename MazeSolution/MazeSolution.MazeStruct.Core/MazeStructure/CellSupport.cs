using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    public class CellSupport
    {
        public static void SetRelation(Cell startCell, Cell nextCell, Directions.DirectionEnum @enum, bool canMove)
        {
            startCell.AddRelation(nextCell, @enum, canMove);
        }
    }
}
