using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MazeSolution.Core.Generators
{
    /// <summary>
    /// Менеджер связей
    /// </summary>
    public static class RelationManager
    {
        /// <summary>
        /// Типы связей
        /// </summary>
        private static HashSet<IRelationType> _relation = new HashSet<IRelationType>()
        {
            new MazeWallRelation(),
            new WallRelation(),
            new Passage(),
            new DestroyedWall()
        };

        /// <summary>
        /// Получить связь определенного типа
        /// </summary>
        /// <typeparam name="T">Тип связи</typeparam>
        /// <returns>Связь</returns>
        public static IRelationType GetRelationType<T>() where T: IRelationType
        {
            return _relation.FirstOrDefault(x => x is T);
        }
    }
}
