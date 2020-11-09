using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    class AccessDenied : BaseCommand
    {
        protected override IMazePackage ExecuteCommand()
        {
            return PackageFactory.AccessDeniedResponse();
        }
    }
}
