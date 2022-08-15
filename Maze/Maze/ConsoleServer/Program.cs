using Autofac;
using Maze.Common;
using Maze.Common.DataExhange;
using Maze.Common.Logging;
using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackageFactory;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.Common;
using Maze.Server.Core;
using Maze.Server.Core.Access;
using Maze.Server.MazeCommands;
using Maze.Server.MazeCommands.MazeCommandsFactory;
using Maze.Server.MazeConfiguration;
using Maze.Server.MazeService.LoginService;
using Maze.Server.MazeService.MessageSenderService;
using Maze.Server.MazeService.SessionService;
using System;

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
        public void Configurate()
        {
            MazeDIContaner.RegisterType<IMazeCommandFactory>(new SimpleCommandFactory());
            MazeDIContaner.RegisterType<IPackageFactory>(new SimplePackageFactory());
            MazeDIContaner.RegisterType<IAccessList>(new SimpleAccessList());
            MazeDIContaner.RegisterType<IMazePackageParser>(new CompressDecorator(new JsonMazePackageParser()));
            MazeDIContaner.RegisterType<IDataExchanger>((c) => new UdpDataExchanger(c.Resolve<IMazePackageParser>()));
        }

        public void Configurate(MazeDIContaner item)
        {
            throw new NotImplementedException();
        }
    }

    class ServiceConfiguration : IServiceConfigurator
    {
        public void Configurate()
        {
            MazeDIContaner.RegisterType<ISessionService>(new DumpSessionService());
            MazeDIContaner.RegisterType<ILoginService>(new SimpleUserService());
            MazeDIContaner.RegisterType<IMessageSenderService>(new UdpDataExchangeMessageSender());
        }

        public void Configurate(MazeDIContaner item)
        {
            throw new NotImplementedException();
        }
    }

    class LogsConfiguration : ILogsConfigurator
    {
        public void Configurate(MazeLogManager logManager)
        {
            logManager.AddLogger(Constants.Loggers.CommonLogger);
        }

        public void Configurate()
        {
            throw new NotImplementedException();
        }
    }
}