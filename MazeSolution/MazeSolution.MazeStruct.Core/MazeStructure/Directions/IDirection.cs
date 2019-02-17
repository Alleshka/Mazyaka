using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public interface IDirection
    {
    }

    public abstract class BaseDirection : IDirection
    {
        protected abstract DirectionEnum _relatedEnum { get; }
    }
}
