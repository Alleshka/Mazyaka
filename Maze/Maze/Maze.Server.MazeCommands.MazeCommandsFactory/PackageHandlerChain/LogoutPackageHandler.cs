using Maze.Common.MazePackages.MazePackages;
using Maze.Server.MazeCommands.MazeCommands;

namespace Maze.Server.MazeCommands.MazeCommandsFactory.PackageHandlerChain
{
    class LogoutPackageHandler : BasePackageHandler<LogoutMazePackage>
    {
        protected override IMazeServerCommand Handle(LogoutMazePackage package)
        {
            return new LogoutCommand(package.SecurityToken);
        }
    }
}
