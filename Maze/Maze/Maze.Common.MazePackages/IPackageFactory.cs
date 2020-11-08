using Maze.Common.MazePackages.MazePackages;
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

    public class SimplePackageFactory : IPackageFactory
    {
        private static SimplePackageFactory _simplePackageFactory = null;
        public static IPackageFactory GetInstance()
        {
            if (_simplePackageFactory == null) _simplePackageFactory = new SimplePackageFactory();
            return _simplePackageFactory;
        }

        public IMazePackage LoginUserRequest(string login, string password)
        {
            return new LoginMazePackage()
            {
                Login = login,
                Password = password
            };
        }

        public IMazePackage AccessDeniedResponse()
        {
            return new AccessDeniedMazePackage();
        }

        public IMazePackage ExceptionMessageResponse(string message)
        {
            return new ExceptionMazePackage(message);
        }

        public IMazePackage LogountUserRequest(string userToken)
        {
            return new LogoutMazePackage(userToken);
        }

        public IMazePackage RegisterUserRequest(string userLogin)
        {
            return new RegisterUserPackage()
            {
                UserLogin = userLogin
            };
        }

        public IMazePackage RegisterUserResponse(MazeUser mazeUser)
        {
            return new RegisterUserResponePackage()
            {
                MazeUser = mazeUser
            };
        }

        public IMazePackage LoginUserResponse(string userToken)
        {
            return new LoginResponceMazePackage()
            {
                UserToken = userToken
            };
        }

        public IMazePackage MessageCommon(string message)
        {
            return new MessageMazePackage()
            {
                Message = message
            };
        }
    }
}
