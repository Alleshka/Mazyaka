using Maze.Common.MazePackages.MazePackages;
using Maze.Server.MazeCommands.MazeCommands;

namespace Maze.Server.MazeCommands.MazeCommandsFactory.PackageHandlerChain
{
    class AccessDeniedPackageHandler : BasePackageHandler<AccessDeniedMazePackage>
    {
        protected override IMazeServerCommand Handle(AccessDeniedMazePackage package)
        {
            return new AccessDenied();
        }
    }
}
