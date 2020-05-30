using Maze.Common.MazaPackages;
using Maze.Common.MazaPackages.Packages;
using Maze.Common.MazaPackages.Parsers;
using Maze.Server.Core;
using Maze.Server.UdpServer;
using System;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = SimpleMazeServer.Instance;
            server.Start(1433);
        }
    }
}
