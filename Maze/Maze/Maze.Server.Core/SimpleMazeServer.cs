using Autofac;
using Maze.Common;
using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Server.Common;
using Maze.Server.Core.QueueHandler;
using Maze.Server.MazeConfiguration;
using System;
using System.Net;

namespace Maze.Server.Core
{
    public class SimpleMazeServer : IMazeServer
    {
        // Очередь команд к исполнению
        private IMazePackageQueueHandler _queue;
        protected IMazePackageQueueHandler Queue
        {
            get
            {
                // Это не вынесено в конфигурацию, потому что об этом знает только сервер
                // Возможно, стоит сделать "расширение" конфигов
                return _queue ??= new SimpleMazePackageQueueHandler();
            }
        }

        private IDataExchanger _dataExchanger;
        protected IDataExchanger DataExchanger
        {
            get
            {
                return _dataExchanger;
            }
        }

        public SimpleMazeServer(IImplementationConfigurator implementationConfigurator, IServiceConfigurator serviceConfigurator, ILogsConfigurator logsConfigurator)
        {
            // TODO: Вынести в обёртку, которая всё сделала
            implementationConfigurator.Configurate();
            serviceConfigurator.Configurate();
            logsConfigurator.Configurate(MazeLogManager.Instance);

            _dataExchanger = MazeDIContaner.Get<IDataExchanger>();
            _dataExchanger.OnRecieveMessage += ReceiveMessage;
        }

        public void Start(int port)
        {
            DataExchanger.Start(port);
            Queue.Start();
        }

        public void Stop()
        {
            Queue.Stop();
            DataExchanger.Stop();
        }

        protected void ReceiveMessage(IDataExchanger sender, DataExchengerEventArgs args)
        {
            Queue.AddPackage(args.Package, args.IPEndPoint);
        }
    }
}
