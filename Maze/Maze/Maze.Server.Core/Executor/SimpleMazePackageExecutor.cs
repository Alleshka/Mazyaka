using Maze.Common;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Server.Common;
using Maze.Server.MazeCommands;
using Maze.Server.MazeService.MessageSenderService;
using System.Net;

namespace Maze.Server.Core.Executor
{
    internal class SimpleMazePackageExecutor : IMazePackageExecutor
    {
        private IMazeCommandFactory _commandFactory;

        public SimpleMazePackageExecutor()
        {
            _commandFactory = MazeDIContaner.Get<IMazeCommandFactory>();
        }

        public void Execute(IMazePackage package, IPEndPoint endPoint)
        {
            MazeLogManager.Instance.Write($"Обработка пакета {package.TypeName}", () =>
            {
                var cmd = _commandFactory.CreateCommand(package);
                var result = cmd.Execute();
                MazeDIContaner.Get<IMessageSenderService>().SendMessage(result, endPoint);
            }, Constants.Loggers.CommonLogger);
        }
    }
}
