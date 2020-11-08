using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Commands;
using Maze.Server.Commands.Commands;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.SessionStorage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.Core.PackageHandlerChain
{
    /// <summary>
    /// Обработка пакета с данными для входа
    /// </summary>
    internal class LoginPackageHandler : BasePackageHandlerWithUsersService<LoginMazePackage>
    {
        protected override IMazeServerCommand Handle(LoginMazePackage package)
        {
            return new LoginCommand(SessionStorage, UserRepository, package.Login);
        }
    }
}
