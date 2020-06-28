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
        IMazePackage LoginPackage(string login, string password);
        IMazePackage HelloWorldPackage();
    }

    public class SimplePackageFactory : IPackageFactory
    {
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
    }

}
