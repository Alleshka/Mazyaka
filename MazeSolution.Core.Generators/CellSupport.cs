using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.Generators
{
    public class CellSupport
    {
        public static IRelation GetCellsRelation(ICell startCell, ICell nextCell)
        {
            IRelation relation = null;
            foreach (var rel in startCell.AllRelations)
            {                
                if (rel.Value != null && rel.Value.GetNextCell != null && ReferenceEquals(rel.Value.GetNextCell, nextCell))
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
