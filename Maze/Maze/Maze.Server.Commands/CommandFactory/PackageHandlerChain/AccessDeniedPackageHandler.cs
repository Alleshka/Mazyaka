using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Commands.Commands;
using Maze.Server.Core.PackageHandlerChain;

namespace Maze.Server.Commands.CommandFactory.PackageHandlerChain
{
    class AccessDeniedPackageHandler : BasePackageHandler<AccessDeniedMazePackage>
    {
        protected override IMazeServerCommand Handle(AccessDeniedMazePackage package)
        {
            return new AccessDenied();
        }
    }
}
