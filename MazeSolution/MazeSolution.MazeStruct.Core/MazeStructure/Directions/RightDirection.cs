using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public class RightDirection : IDirection
    {
        public bool AddRelation(Cell startCell, Cell nextCell, bool CanMove)
        {
            var relation = new Relation(startCell, nextCell, CanMove);
            startCell.RightRelation = relation;
            return true;
        }

        public bool RemoveRelation(Cell curCell)
        {
            curCell.RightRelation = null;
            return true;
        }

        public void SetMoveStatus(Cell curCell, bool canMoveStatus)
        {
            curCell.RightRelation.CanMove = canMoveStatus;
        }
    }
}
