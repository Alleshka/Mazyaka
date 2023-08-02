using Maze.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    internal abstract class BaseMazeSite : IMazeSite
    {
        public virtual MoveResult Enter(IMazePlayer player)
        {
            return new MoveResult()
            {
                Column = player.Col,
                Line = player.Line,
                MazeSite = this.GetType().Name,
                Status = MoveStatus.Success
            };
        }
    }
}
