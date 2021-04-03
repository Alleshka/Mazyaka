using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure.Implementatioms
{
    public class BaseMazeExit : BaseBlock, IMazeExit
    {
        public override void MoveObject(ILiveGameObject gameObject, IMazeDirection direction)
        {
            base.MoveObject(gameObject, direction);
            Logging.MazeLogManager.Instance.Write("Нашёл выход Pog", Constants.Loggers.CommonLogger);
        }
    }
}
