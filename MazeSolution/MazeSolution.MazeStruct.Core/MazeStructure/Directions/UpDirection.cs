using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.Directions
{
    /// <summary>
    /// Направление наверх
    /// </summary>
    public class UpDirection : BaseDirection
    {
        protected override DirectionEnum _relatedEnum => DirectionEnum.Up;
    }
}
