using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MazeSolution.Core.Generators
{
    public static class RelationManager
    {
        private static HashSet<IRelationType> _relation = new HashSet<IRelationType>()
        {
            new MazeWallRelation(),
            new WallRelation(),
            new None(),
            new DestroyedWall()
        };

        public static IRelationType GetRelationType<T>()
        {
            return _relation.FirstOrDefault(x => x is T);
        }
    }
}
