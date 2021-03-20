using Autofac;
using Maze.Common;
using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Server.AutofacContainer;
using Maze.Server.Core.QueueHandler;
using Maze.Server.MazeConfiguration;
using Maze.Server.UdpServer;
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

        public SimpleMazeServer(IImplementationConfigurator implementationConfigurator, IServiceConfigurator serviceConfigurator, ILogsConfigurator logsConfigurator, IDataExchangeConfigurator dataExchangeConfigurator)
        {
            implementationConfigurator.Configurate(MazeAutofacContainer.Instance);

            serviceConfigurator.Configurate(MazeAutofacContainer.Instance);
            logsConfigurator.Configurate(MazeLogManager.Instance);

            MazeAutofacContainer.Instance.AddImplementation<IDataExchanger>((c) => new MazeUdpDataExchange(c.Resolve<IMazePackageParser>(), ReceiveMessage));
            MazeAutofacContainer.Instance.Build();

            // TODO: Плохо, придумать что сделать
            _dataExchanger = MazeAutofacContainer.Instance.GetImplementation<IDataExchanger>();
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

        protected void ReceiveMessage(IMazePackage package, IPEndPoint endPoint, IDataExchanger sender)
        {
            Queue.AddPackage(package, endPoint);
        }
    }
}
