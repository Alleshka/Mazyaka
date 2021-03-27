using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure.Directions
{
    public class BaseDirection : IMazeDirection
    {
        private BaseDirection _instance;
        public IMazeDirection Direction
        {
            get
            {
                return _instance ??= new BaseDirection();
            }
        }

        protected BaseDirection()
        {

        }
    }
}
