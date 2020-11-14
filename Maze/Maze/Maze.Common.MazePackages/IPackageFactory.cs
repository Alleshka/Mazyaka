using Maze.Common.Model;
using System;

namespace Maze.Common.MazePackages
{
    /// <summary>
    /// Фабрика пакетов для того, чтобы скрыть все пакеты от лишних взаимодействий
    /// </summary>
    public interface IPackageFactory
    {
        IMazePackage RegisterUserRequest(string userLogin);
        IMazePackage RegisterUserResponse(MazeUser mazeUser);

        IMazePackage LoginUserRequest(string login, string password);
        IMazePackage LoginUserResponse(string userToken);

        IMazePackage LogountUserRequest(string userToken);

        IMazePackage AccessDeniedResponse();
        IMazePackage ExceptionMessageResponse(string message);

        IMazePackage MessageCommon(string message);
    }
}
