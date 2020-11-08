using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Commands;
using Maze.Server.Commands.Commands;

namespace Maze.Server.Core.PackageHandlerChain
{
    /// <summary>
    /// Обработка пакета с данными для входа
    /// </summary>
    internal class LoginPackageHandler : BasePackageHandler<LoginMazePackage>
    {
        protected override IMazeServerCommand Handle(LoginMazePackage package)
        {
            return new LoginCommand(package.Login);
        }
    }
}
