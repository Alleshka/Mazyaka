using Maze.Common.MazePackages;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Access
{
    public interface IAccessList
    {
        bool HasAccess(IMazePackage package, MazeUserRole role);
    }
}
