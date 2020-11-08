using System;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет для прерывания пользовательской сессии
    /// </summary>
    internal class LogoutMazePackage : BaseMazePackage
    {
        public String UserToken { get; set; }

        public LogoutMazePackage()
        {

        }

        public LogoutMazePackage(string userToken)
        {
            UserToken = userToken;
        }
    }
}
