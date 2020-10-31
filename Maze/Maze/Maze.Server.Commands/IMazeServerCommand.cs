using Maze.Common.MazePackages;
using System;
using System.Threading;

namespace Maze.Server.Commands
{
    /// <summary>
    /// Интерфейс команды
    /// </summary>
    public interface IMazeServerCommand
    {
        IMazePackage Execute();
    }

    public abstract class BaseCommand : IMazeServerCommand
    {
        protected IPackageFactory PackageFactory { get => SimplePackageFactory.GetInstance(); }

        public abstract IMazePackage Execute();
    }
}
