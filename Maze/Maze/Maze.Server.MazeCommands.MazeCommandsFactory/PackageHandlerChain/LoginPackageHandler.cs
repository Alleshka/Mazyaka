using Maze.Common.MazePackages.MazePackages;
using Maze.Server.MazeCommands.MazeCommands;

namespace Maze.Server.MazeCommands.MazeCommandsFactory.PackageHandlerChain
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
