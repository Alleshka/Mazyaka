using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public class LeftDirection : IDirection
    {
        public bool AddRelation(Cell startCell, Cell nextCell, bool CanMove)
        {
            var relation = new Relation(startCell, nextCell, CanMove);
            startCell.LeftRelation = relation;
            return true;
        }

        public bool RemoveRelation(Cell curCell)
        {
            curCell.LeftRelation = null;
            return true;
        }

        public void SetMoveStatus(Cell curCell, bool canMoveStatus)
        {
            curCell.LeftRelation.CanMove = canMoveStatus;
        }
    }
}
