using Maze.Common.MazePackages;
using Maze.Server.Core.CommandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

            var server = SimpleMazeServer.Instance;


            server.DataExchanger.SendMessage(result, endPoint);
            // TODO: Отправка ответа обратно
        }
    }
}
