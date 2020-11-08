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
    class CreateUserPachageHandler : BasePackageHandlerWithUsersService<RegisterUserPackage>
    {
        protected override IMazeServerCommand Handle(RegisterUserPackage package)
        {
            return new CreateUserCommand(SessionStorage, UserRepository, package.UserLogin);
        }
    }
}
