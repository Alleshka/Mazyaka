using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazePackages.MazePackages
{
    internal class CreateUserPackage : BaseMazePackage
    {
        public string UserLogin { get; set; }

        public CreateUserPackage()
        {
        }
    }

    internal class CreateUserAnswerPackage : BaseMazePackage
    {
        public Guid UserID { get; set; }
    }
}
