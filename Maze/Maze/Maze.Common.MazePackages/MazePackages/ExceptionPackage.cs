using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazePackages.MazePackages
{
    internal class ExceptionPackage : BaseMazePackage
    {

        public String Message { get; set; }

        public ExceptionPackage(string message)
        {
            Message = message;
        }
    }
}
