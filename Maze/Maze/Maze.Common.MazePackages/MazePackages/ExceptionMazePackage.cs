using System;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет с сообщением об исключении
    /// </summary>
    internal class ExceptionMazePackage : BaseMazePackage
    {
        public String Message { get; set; }

        public ExceptionMazePackage()
        {

        }

        public ExceptionMazePackage(string message)
        {
            Message = message;
        }
    }
}
