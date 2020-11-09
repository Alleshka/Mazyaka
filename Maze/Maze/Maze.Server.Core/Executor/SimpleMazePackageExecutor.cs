using Maze.Common;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Core.CommandFactory;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.MazeService.MessageSender;
using System.Net;

namespace Maze.Server.Core.Executor
{
    internal class SimpleMazePackageExecutor : IMazePackageExecutor
    {
        private SimpleCommandFactory _commandFactory;

        public SimpleMazePackageExecutor()
        {
            _commandFactory = new SimpleCommandFactory();
        }

        public void Execute(IMazePackage package, IPEndPoint endPoint)
        {
            MazeLogManager.Instance.Write($"Обработка пакета {package.TypeName}", () =>
            {
                var cmd = _commandFactory.CreateCommand(package);
                var result = cmd.Execute();
                MazeServiceStorage.Instance.GetService<IMessageSenderService>().SendMessage(result, endPoint);
            }, Constants.Loggers.CommonLogger);
        }
    }
}
