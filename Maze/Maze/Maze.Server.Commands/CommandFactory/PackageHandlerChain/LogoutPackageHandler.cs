using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Commands.Commands;
using Maze.Server.Core.PackageHandlerChain;

namespace Maze.Server.Commands.CommandFactory.PackageHandlerChain
{
    class LogoutPackageHandler : BasePackageHandler<LogoutMazePackage>
    {
        protected override IMazeServerCommand Handle(LogoutMazePackage package)
        {
            return new LogoutCommand(package.SecurityToken);
        }
    }
}
