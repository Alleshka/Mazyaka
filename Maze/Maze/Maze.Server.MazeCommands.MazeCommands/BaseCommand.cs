using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.Logging;
using System.Runtime.CompilerServices;
using Maze.Server.AutofacContainer;

[assembly: InternalsVisibleTo("Maze.Server.MazeCommands.MazeCommandsFactory")]
namespace Maze.Server.MazeCommands.MazeCommands
{
    abstract class BaseCommand : IMazeServerCommand
    {
        private IPackageFactory _packageFactory;
        protected IPackageFactory PackageFactory { get => _packageFactory ??= MazeAutofacContainer.Instance.GetImplementation<IPackageFactory>(); }

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