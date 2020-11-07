using Maze.Common.MazePackages.MazePackages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Common.MazePackages
{
    /// <summary>
    /// Фабрика пакетов для того, чтобы скрыть все пакеты от лишних взаимодействий
    /// </summary>
    public interface IPackageFactory
    {
        IMazePackage CreateUser(string userLogin);
        IMazePackage CreateUserAnswer(Guid userID);

        IMazePackage LoginPackage(string login, string password);
        IMazePackage LoginAnswerPackage(string userToken);

        IMazePackage LogoutPackage(string userToken);

        IMazePackage HelloWorldPackage();
        IMazePackage HasNotAccessPackage();
        IMazePackage ExceptionPackage(string message);
    }

    public class SimplePackageFactory : IPackageFactory
    {
        private static SimplePackageFactory _simplePackageFactory = null;
        public static IPackageFactory GetInstance()
        {
            if (_simplePackageFactory == null) _simplePackageFactory = new SimplePackageFactory();
            return _simplePackageFactory;
        }

        public IMazePackage LoginPackage(string login, string password)
        {
            return new LoginMazePackage()
            {
                Login = login,
                Password = password
            };
        }

        public IMazePackage HelloWorldPackage()
        {
            return new HelloWorldPackage();
        }

        public IMazePackage HasNotAccessPackage()
        {
            return new HasNotAccessPackage();
        }

        public IMazePackage ExceptionPackage(string message)
        {
            return new ExceptionPackage(message);
        }

        public IMazePackage LogoutPackage(string userToken)
        {
            return new LogoutMazePackage()
            {
                UserToken = userToken
            };
        }

        public IMazePackage CreateUser(string userLogin)
        {
            return new CreateUserPackage()
            {
                UserLogin = userLogin
            };
        }

        public IMazePackage CreateUserAnswer(Guid userID)
        {
            return new CreateUserAnswerPackage()
            {
                UserID = userID
            };
        }

        public IMazePackage LoginAnswerPackage(string userToken)
        {
            return new LoginAnswerMazePackage()
            {
                UserToken = userToken
            };
        }
    }

}
