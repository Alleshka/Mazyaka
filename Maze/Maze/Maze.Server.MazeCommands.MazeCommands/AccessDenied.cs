using Maze.Common.MazePackages;

namespace Maze.Server.MazeCommands.MazeCommands
{
    class AccessDenied : BaseCommand
    {
        protected override IMazePackage ExecuteCommand()
        {
            return PackageFactory.AccessDeniedResponse();
        }
    }
}
