using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.Core.GameObjects;

namespace MazeSolution.Core.MazeStructrure
{
    /// <summary>
    /// Тип связи
    /// </summary>
    public interface IRelationType
    {
        /// <summary>
        ///  Возможно ли идти по это 
        /// </summary>
        bool CanMove { get; }


        bool CanDestroy { get; }
    }

    /// <summary>
    /// Тип связи - стена лабиринта
    /// </summary>
    public class MazeWallRelation : IRelationType
    {
        bool IRelationType.CanMove => false;

        bool IRelationType.CanDestroy => false;
    }

    /// <summary>
    /// Стена
    /// </summary>
    public class WallRelation : IRelationType
    {
        public bool CanMove => false;

        public bool CanDestroy => true;
    }

    /// <summary>
    /// Ничего
    /// </summary>
    public class None : IRelationType
    {
        public bool CanMove => true;

        public bool CanDestroy => true;
    }

    public class DestroyedWall : IRelationType
    {
        public bool CanMove => true;

        public bool CanDestroy => false;
    }

    public class MazeExit : IRelationType
    {
        public bool CanMove => true;

        public bool CanDestroy => false;
    }
}
