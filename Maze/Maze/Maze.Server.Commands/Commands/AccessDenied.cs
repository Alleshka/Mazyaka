using Maze.Common.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    class AccessDenied : BaseCommand
    {
        public override IMazePackage Execute()
        {
            return PackageFactory.AccessDeniedResponse();
        }
    }
}
