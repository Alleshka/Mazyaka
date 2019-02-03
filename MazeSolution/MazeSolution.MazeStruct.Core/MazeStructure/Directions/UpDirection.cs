using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    /// <summary>
    /// Направление наверх
    /// </summary>
    public class UpDirection : IDirection
    {
        public bool AddRelation(Cell startCell, Cell nextCell, bool CanMove)
        {
            var relation = new Relation(startCell, nextCell, CanMove);
            startCell.UpRelation = relation;
            return true;
        }

        public bool RemoveRelation(Cell curCell)
        {
            curCell.UpRelation = null;
            return true;
        }

        public void SetMoveStatus(Cell curCell, bool canMoveStatus)
        {
            curCell.UpRelation.CanMove = canMoveStatus;
        }
    }
}
