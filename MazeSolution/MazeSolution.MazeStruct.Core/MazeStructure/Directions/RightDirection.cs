using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public class RightDirection : BaseDirection
    {
        protected override DirectionEnum _relatedEnum => DirectionEnum.Right;
    }
}
