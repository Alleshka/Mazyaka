using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Server.Core.Access
{
    public class SimpleAccessList : IAccessList
    {
        private static readonly Dictionary<Type, IEnumerable<string>> _acceses = new Dictionary<Type, IEnumerable<string>>()
        {
            { typeof(LoginMazePackage), Constants.Roles.ALL },
            { typeof(CreateUserPackage), Constants.Roles.ALL },
            { typeof(LogoutMazePackage), Constants.Roles.NOT_GUEST },
        };

        public bool HasAccess(IMazePackage package, MazeUserRole role)
        {
            return _acceses.TryGetValue(package.GetType(), out var roles) && roles.Any(x => x == role?.RoleName);
        }
    }
}
