﻿using Maze.Common.MazePackages.Packages;
using Maze.Server.MazeCommands.MazeCommands;

namespace Maze.Server.MazeCommands.MazeCommandsFactory.PackageHandlerChain
{
    class CreateUserPachageHandler : BasePackageHandler<RegisterUserPackage>
    {
        protected override IMazeServerCommand Handle(RegisterUserPackage package)
        {
            return new CreateUserCommand(package.UserLogin);
        }
    }
}
