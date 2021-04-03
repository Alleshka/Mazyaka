using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Common.Logging;
using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;

namespace Maze.Common.MazeStructure.Implementatioms
{
    public class BaseMazeWall : BaseBlock, IMazeWall
    {
        public override void MoveObject(ILiveGameObject gameObject, IMazeDirection direction)
        {
            base.MoveObject(gameObject, direction);
            MazeLogManager.Debug("Нашёл стену лабиринта Pog");
        }
    }
}
