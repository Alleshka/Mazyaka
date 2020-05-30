using Maze.Common.MazaPackages.Packages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.Core.PackageHandlerChain
{
    /// <summary>
    /// Обработка пакета с данными для входа
    /// </summary>
    internal class LoginPackageHandler : BasePackageHandler<LoginMazePackage>
    {
        protected override void Handle(LoginMazePackage package)
        {
            Console.WriteLine(package.ToString());
        }
    }
}
