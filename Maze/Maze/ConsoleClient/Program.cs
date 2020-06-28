using Maze.Common.MazePackages;
using Maze.Common.MazePackages.Parsers;
using Maze.Server.UdpServer;
using System;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var packageFactory = new SimplePackageFactory();
            var localPort = 1444;
            var remotePort = 1433;

            using (var sender = new MazeUdpDataExchange(localPort, new JsonCompressedMazePackageParser(), (message, remoteIp, messageSender) =>
             {
                 Console.WriteLine($"Message from server ({remoteIp.Address}:{remoteIp.Port}): {message.ToString()}");
             }))
            {
                sender.Start();

                while (true)
                {
                    Console.WriteLine("enter number");
                    Console.WriteLine("1 - LoginCommand");
                    Console.WriteLine("2 - HelloWorldCommand");

                    if (int.TryParse(Console.ReadLine(), out int text))
                    {
                        switch (text)
                        {
                            case 1:
                                {
                                    sender.SendMessage(packageFactory.LoginPackage("alleshka", "123qwe"), "127.0.0.1", remotePort);
                                    break;
                                }
                            case 2:
                                {
                                    sender.SendMessage(packageFactory.HelloWorldPackage(), "127.0.0.1", remotePort);
                                    break;
                                }
                        }
                    }
                }
            }
        }
    }
}
