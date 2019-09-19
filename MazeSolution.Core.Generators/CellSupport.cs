using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.Generators
{
    /// <summary>
    /// Методы расширения для ячеек лабиринта
    /// </summary>
    public static class CellsHelpers
    {
        /// <summary>
        /// Находит объект связи между ячейками
        /// </summary>
        /// <param name="startCell">Ячейка откуда проверяется связь</param>
        /// <param name="nextCell">Ячейка куда проверяется связь</param>
        /// <returns>Объект связи если существует</returns>
        public static IRelation GetCellsRelation(this ICell startCell, ICell nextCell)
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

            return relation ?? throw new Exception("Не удалось найти связь между ячейками");
        }
    }
}
