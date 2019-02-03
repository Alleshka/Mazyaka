using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    public class LeftDirection : BaseDirection
    {
        protected override DirectionEnum _relatedEnum => DirectionEnum.Left;
    }
}
