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

        public static Relation GetCellsRelation(Cell startCell, Cell nextCell)
        {
            Relation relation = null;
            foreach(var rel in startCell._relations)
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
