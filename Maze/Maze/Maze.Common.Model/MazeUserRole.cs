using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.Model
{
    public class MazeUserRole : BaseMazeObject
    {
        public string RoleName { get; }

        public  MazeUserRole()
        {

        }

        public MazeUserRole(string roleName)
        {
            RoleName = roleName;
        }
    }
}
