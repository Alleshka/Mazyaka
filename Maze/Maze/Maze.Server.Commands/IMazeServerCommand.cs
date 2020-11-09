using Maze.Common;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
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

        public IMazePackage Execute()
        {
            return MazeLogManager.Instance.Write($"Выполнение комманды {this.GetType().Name}", () =>
            {
                return ExecuteCommand();
            }, Constants.Loggers.CommonLogger);
        }
        
        protected abstract IMazePackage ExecuteCommand();
    }
}
