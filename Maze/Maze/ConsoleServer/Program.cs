using Maze.Server.UdpServer;
using System;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int localPort = 1433;
            var server = new MazeUdpDataExchange(1433, (message, remoteIp, messageSender) =>
            {
                Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: \"{message}\" from {remoteIp.Address} ({remoteIp.Port})");
                messageSender.SendMessage($"I got your message ({message}) at {DateTime.Now.ToShortTimeString()} from {remoteIp.Address} ({remoteIp.Port})", remoteIp);
            });

            object t = null;

            server.Start();
        }
    }
}
