using Maze.Common.MazePackages;

namespace Maze.Server.MazeCommands
{
    public interface IMazeCommandFactory
    {
        IMazeServerCommand CreateCommand(IMazePackage mazePackage);
    }
}
