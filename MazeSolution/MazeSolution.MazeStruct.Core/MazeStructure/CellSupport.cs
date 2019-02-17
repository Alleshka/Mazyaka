using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure
{
    public class CellSupport
    {
        public static void SetRelation(BaseCell startCell, BaseCell nextCell, Directions.DirectionEnum @enum, bool canMove)
        {
            startCell.AddRelation(nextCell, @enum, canMove);
        }

        public static Relation GetCellsRelation(BaseCell startCell, BaseCell nextCell)
        {
            Relation relation = null;
            foreach(var rel in startCell.Relations)
            {
                if (rel.Value != null && rel.Value.NextCell != null && rel.Value.NextCell.Equals(nextCell))
                {
                    relation = rel.Value;
                    break;
                }
            }

            if (relation != null)
            {
                return relation;
            }
            else throw new Exception("Не удалось найти связь между ячейками");
        }
    }
}
