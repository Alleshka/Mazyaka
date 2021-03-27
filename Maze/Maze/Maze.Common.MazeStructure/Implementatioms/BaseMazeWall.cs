using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Common.Logging;

namespace Maze.Common.MazeStructure.Implementatioms
{
    public class BaseMazeWall : BaseBlock, IMazeWall
    {
        public override void MoveAction()
        {
            MazeLogManager.Debug("Нашёл стену лабиринта Pog");
        }
    }
}
