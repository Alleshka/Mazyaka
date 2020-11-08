﻿using Maze.Common.MazePackages;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.Commands;
using Maze.Server.Core.PackageHandlerChain;
using Maze.Server.Core.PackageQueue;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionStorage;
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
        private IMazePackageQueueHandler _queue;

        private MazeUdpDataExchange _dataExchanger;
        public MazeUdpDataExchange DataExchanger
        {
            get => _dataExchanger;
        }

        private SimpleMazeServer()
        {
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
            if (_dataExchanger == null)
            {
                _dataExchanger = new MazeUdpDataExchange(port, CreateParser(), ReceiveMessage);
            }
            _dataExchanger.Start();
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

            serviceStorage.AddService<ISessionStorage>(new DumpSessionStorage());
        }
    }
}
