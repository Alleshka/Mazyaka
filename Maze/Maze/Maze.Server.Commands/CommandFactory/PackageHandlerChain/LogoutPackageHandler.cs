using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Commands.Commands;
using Maze.Server.Core.PackageHandlerChain;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Commands.CommandFactory.PackageHandlerChain
{
    class LogoutPackageHandler : BasePackageHandler<LogoutMazePackage>
    {
        public LogoutPackageHandler(ISessionStorage sessionStorage, IUserRepository userRepository) : base(sessionStorage, userRepository)
        {
        }

        protected override IMazeServerCommand Handle(LogoutMazePackage package)
        {
            return new LogoutCommand(SessionStorage, package.SecurityToken);
        }
    }
}
