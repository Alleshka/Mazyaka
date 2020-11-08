using Maze.Common;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.Core.PackageQueue;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionService;
using Maze.Server.MazeService.MessageSender;
using Maze.Server.UdpServer;
using System.Net;

namespace Maze.Server.Core
{
    public class SimpleMazeServer : IMazeServer
    {

        // Очередь команд к исполнению
        private IMazePackageQueueHandler _queue;

        private MazeUdpDataExchange _dataExchanger;
        protected MazeUdpDataExchange DataExchanger
        {
            get
            {
                return _dataExchanger ?? (_dataExchanger = new MazeUdpDataExchange(CreateParser(), ReceiveMessage));
            }
        }

        private SimpleMazeServer()
        {
            ConfigureServices();
            ConfigureLogs();

            _queue = new SimpleMazePackageQueueHandler();
        }

        private static SimpleMazeServer _instance;
        public static SimpleMazeServer Instance
        {
            get
            {
                return _instance ?? (_instance = new SimpleMazeServer());
            }
        }

        public void Start(int port)
        {
            DataExchanger.Start(port);
            _queue.Start();
        }

        public void Stop()
        {
            _queue.Stop();
            _dataExchanger.Stop();
        }

        protected void ReceiveMessage(IMazePackage package, IPEndPoint endPoint, MazeUdpDataExchange sender)
        {
            _queue.AddPackage(package, endPoint);
        }

        private IMazePackageParser CreateParser()
        {
            return new JsonCompressedMazePackageParser();
        }

        private void ConfigureServices()
        {
            var serviceStorage = MazeServiceStorage.Instance;

            serviceStorage.AddService<ISessionService>(new DumpSessionService());
            serviceStorage.AddService<IUserService>(new SimpleUserService());
            serviceStorage.AddService<IMessageSenderService>(new UdpDataExchangeMessageSender(DataExchanger));
        }

        private void ConfigureLogs()
        {
            var logManager = MazeLogManager.Instance;
            logManager.AddLogger(Constants.Loggers.CommonLogger);
        }
    }
}
