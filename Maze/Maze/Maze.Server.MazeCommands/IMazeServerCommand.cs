using Maze.Common.MazePackages;

namespace Maze.Server.MazeCommands
{
    /// <summary>
    /// Интерфейс команды
    /// </summary>
    public interface IMazeServerCommand
    {
        IMazePackage Execute();
    }
}
