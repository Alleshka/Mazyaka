using Maze.Common.MazePackages;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Validation
{
    interface IAccessValidator
    {
        IMazePackage Validate(IMazePackage package, MazeUserRole role);
    }
}
