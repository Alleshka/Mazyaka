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
    class AccessDeniedPackageHandler : BasePackageHandler<HasNotAccessPackage>
    {
        public AccessDeniedPackageHandler(ISessionStorage sessionStorage, IUserRepository userRepository) : base(sessionStorage, userRepository)
        {
        }

        protected override IMazeServerCommand Handle(HasNotAccessPackage package)
        {
            return new AccessDenied();
        }
    }
}
