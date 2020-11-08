using Maze.Common.MazePackages;
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
            var cmd = _commandFactory.CreateCommand(package);
            var result = cmd.Execute();
            MazeServiceStorage.Instance.GetService<IMessageSenderService>().SendMessage(result, endPoint);
        }
    }
}
