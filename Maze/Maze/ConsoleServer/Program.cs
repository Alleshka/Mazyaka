using Maze.Common;
using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackageFactory;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.Core;
using Maze.Server.Core.Access;
using Maze.Server.Core.QueueHandler;
using Maze.Server.ImplementationStorage;
using Maze.Server.MazeCommands;
using Maze.Server.MazeCommands.MazeCommandsFactory;
using Maze.Server.MazeConfiguration;
using Maze.Server.MazeService.LoginService;
using Maze.Server.MazeService.MessageSenderService;
using Maze.Server.MazeService.SessionService;
using Maze.Server.ServiceStorage;
using Maze.Server.UdpServer;
using System;
using System.Net;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new SimpleMazeServer(new ImplementationConfiguration(), new ServiceConfiguration(), new LogsConfiguration(), new DataExchangeConfiguration());
            server.Start(1433);
        }
    }

    class ImplementationConfiguration : IImplementationConfigurator
    {
        public void Configurate(MazeImplementationStorage item)
        {
            item.Bind<IMazeCommandFactory>(new SimpleCommandFactory(), ImplementationMode.Single);
            item.Bind<IPackageFactory>(new SimplePackageFactory(), ImplementationMode.Single);
            item.Bind<IAccessList>(new SimpleAccessList(), ImplementationMode.Single);
            item.Bind<IMazePackageParser>(new JsonCompressedMazePackageParser(), ImplementationMode.Single);
        }
    }

    class ServiceConfiguration : IServiceConfigurator
    {
        public void Configurate(MazeServiceStorage serviceStorage)
        {
            serviceStorage.AddService<ISessionService>(new DumpSessionService());
            serviceStorage.AddService<ILoginService>(new SimpleUserService());
            serviceStorage.AddService<IMessageSenderService>(new UdpDataExchangeMessageSender());
        }
    }

    class LogsConfiguration : ILogsConfigurator
    {
        public void Configurate(MazeLogManager logManager)
        {
            logManager.AddLogger(Constants.Loggers.CommonLogger);
        }
    }

    class DataExchangeConfiguration : IDataExchangeConfigurator
    {
        public IDataExchanger Create(IMazePackageParser packageParser, Action<IMazePackage, IPEndPoint, IDataExchanger> onReceiveMessage)
        {
            return new MazeUdpDataExchange(packageParser, onReceiveMessage);
        }
    }
}