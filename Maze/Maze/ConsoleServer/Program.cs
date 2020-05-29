using Maze.Server.UdpServer;
using System;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int localPort = 1433;
            var server = new MazeUdpDataExchange(1433, new SimplePackageParser(), (message, remoteIp, messageSender) =>
            {
                Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: \"{message}\" from {remoteIp.Address} ({remoteIp.Port})");
                messageSender.SendMessage(new LoginMazePackage($"I got your message ({message}) at {DateTime.Now.ToShortTimeString()} from {remoteIp.Address} ({remoteIp.Port})", "fuuuuu"), remoteIp);
            });

            object t = null;

            server.Start();
        }
    }
}
