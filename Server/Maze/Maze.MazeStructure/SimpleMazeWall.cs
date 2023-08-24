using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    internal class SimpleMazeWall : BaseMazeSite, IMazeWall
    {
        public bool IsDestroyed { get; protected set; } = false;

        public bool CanDestroy => true;

        public override MoveResult Enter(IMazePlayer player)
        {
            var result = base.Enter(player);
            result.Status = MoveStatus.Failure;
            return result;
        }
    }
}
