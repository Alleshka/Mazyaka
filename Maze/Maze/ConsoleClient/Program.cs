using Maze.Server.UdpServer;
using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var localPort = 1444;
            var remotePort = 1433;

            using (var sender = new MazeUdpDataExchange(localPort, (message, remoteIp, messageSender) =>
             {
                 Console.WriteLine($"Message from server ({remoteIp.Address}:{remoteIp.Port}): {message}");
             }))
            {
                sender.Start();
                while (true)
                {
                    Console.WriteLine("enter your text");
                    var text = Console.ReadLine();
                    if (text == "-1")
                    {
                        sender.Stop();
                        break;
                    }
                    else
                    {
                        sender.SendMessage(text, "127.0.0.1", remotePort);
                    }
                }
            }
        }
    }
}
