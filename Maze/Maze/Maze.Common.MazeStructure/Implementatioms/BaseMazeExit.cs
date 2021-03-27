using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure.Implementatioms
{
    public class BaseMazeExit : BaseBlock, IMazeExit
    {
        public override bool CanMove => true;

        public override void MoveAction()
        {
            Logging.MazeLogManager.Instance.Write("Нашёл стену Pog", Constants.Loggers.CommonLogger);
        }
    }
}
