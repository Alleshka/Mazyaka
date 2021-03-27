using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure.Implementatioms
{
    public class BaseWall : BaseBlock, IWall
    {
        public override bool CanDestroy => true;
    }
}
