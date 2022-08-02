using Autofac;
using Maze.Common;
using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackageFactory;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.AutofacContainer;
using Maze.Server.Core;
using Maze.Server.Core.Access;
using Maze.Server.MazeCommands;
using Maze.Server.MazeCommands.MazeCommandsFactory;
using Maze.Server.MazeConfiguration;
using Maze.Server.MazeService.LoginService;
using Maze.Server.MazeService.MessageSenderService;
using Maze.Server.MazeService.SessionService;
using System;
using System.Net;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new SimpleMazeServer(new ImplementationConfiguration(), new ServiceConfiguration(), new LogsConfiguration());
            server.Start(1433);
        }
    }

    class ImplementationConfiguration : IImplementationConfigurator
    {
        public void Configurate(MazeAutofacContainer item)
        {
            item.AddImplementation<IMazeCommandFactory>(new SimpleCommandFactory());
            item.AddImplementation<IPackageFactory>(new SimplePackageFactory());
            item.AddImplementation<IAccessList>(new SimpleAccessList());
            item.AddImplementation<IMazePackageParser>(new CompressDecorator(new JsonMazePackageParser()));
            item.AddImplementation<IDataExchanger>((c) => new UdpDataExchanger(c.Resolve<IMazePackageParser>()));
        }
    }

    class ServiceConfiguration : IServiceConfigurator
    {
        public void Configurate(MazeAutofacContainer serviceStorage)
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
}