using Maze.Server.Core;
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
