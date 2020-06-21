using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.Model
{
    public class MazeUser : BaseMazeObject
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
