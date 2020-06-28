using Maze.Common.MazePackages;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.Commands;
using Maze.Server.Core.PackageHandlerChain;
using Maze.Server.UdpServer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Maze.Server.Core
{

    public class SimpleMazeServer : IMazeServer
    {
        // Очередь команд к исполнению
        private Queue<IMazeServerCommand> _commands;

        private MazeUdpDataExchange _dataExchanger;

        // Цепочка обязанностей
        private IMazePackageHandler _chain;

        private SimpleMazeServer()
        {
            _commands = new Queue<IMazeServerCommand>();
            _chain = CreateChain();
        }

        private static SimpleMazeServer _instance;
        public static SimpleMazeServer Instance
        {
            get
            {
                return _instance ?? (_instance = new SimpleMazeServer());
            }
        }

        public void AddCommand(IMazeServerCommand command)
        {
            _commands.Enqueue(command);
        }

        public void Start(int port)
        {
            if (_dataExchanger == null)
            {
                _dataExchanger = new MazeUdpDataExchange(port, CreateParser(), ReceiveMessage);
            }
            _dataExchanger.Start();
        }

        public void Stop()
        {
            _dataExchanger.Stop();
        }

        protected void ReceiveMessage(IMazePackage package, IPEndPoint endPoint, MazeUdpDataExchange sender)
        {
            // TODO: Добавлять пакеты в коллекцию, а обрабатывать их уже по ходу
            // TODO: 
            _chain.HandlePackage(package);
        }


        private IMazePackageParser CreateParser()
        {
            return new JsonCompressedMazePackageParser();
        }

        private IMazePackageHandler CreateChain()
        {
            var builder = new PackageChainHandlerBuilder();
            builder.AddChain(new LoginPackageHandler());
            builder.AddChain(new HelloWorldPackageHandler());
            return builder.Head;
        }
    }
}
