using Maze.Common.Model;
using System;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет для прерывания пользовательской сессии
    /// </summary>
    internal class LogoutMazePackage : BaseMazePackageRequest
    {
        public String UserToken { get; set; }

        public override MazeUserRole Roles => MazeUserRole.NotGuest;

        public LogoutMazePackage()
        {

        }

        public LogoutMazePackage(string userToken)
        {
            UserToken = userToken;
        }
    }
}
