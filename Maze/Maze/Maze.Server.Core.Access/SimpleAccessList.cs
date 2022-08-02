using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Server.Core.Access
{
    public class SimpleAccessList : IAccessList
    {
        public bool HasAccess(IMazePackageRequest package, MazeUserRole role)
        {
            return package.Roles.HasFlag(role);
        }
    }
}
