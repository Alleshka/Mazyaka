using System;

namespace Maze.Server.Commands
{
    /// <summary>
    /// Интерфейс команды
    /// </summary>
    public interface IMazeServerCommand
    {
        void Execute();
    }
}
