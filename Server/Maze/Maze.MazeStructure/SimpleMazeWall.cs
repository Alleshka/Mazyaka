using Maze.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    internal class SimpleMazeWall : BaseMazeSite, IMazeWall
    {
        public bool CanDestroy => true;

        public bool IsDestroyed { get; protected set; } = false;

        public bool Destroy()
        {
            if (CanDestroy)
            {
                // TODO: Destroy Wall
                IsDestroyed = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override MoveResult Enter(IMazePlayer player)
        {
            var result = base.Enter(player);
            result.Status = MoveStatus.Failure;
            return result;
        }
    }
}
