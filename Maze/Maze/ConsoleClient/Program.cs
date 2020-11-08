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

            string ip = "127.0.0.1";

            using (var sender = new MazeUdpDataExchange(localPort, new JsonCompressedMazePackageParser(), (message, remoteIp, messageSender) =>
             {
                 Console.WriteLine($"Message from server ({remoteIp.Address}:{remoteIp.Port}): {message.ToString()}");
             }))
            {
                sender.Start();

                while (true)
                {

                    Console.WriteLine("1 - CreateUser");
                    Console.WriteLine("2 - Login");
                    Console.WriteLine("3 - Logout");

                    if (int.TryParse(Console.ReadLine(), out int text))
                    {
                        IMazePackage package = null;
                        switch (text)
                        {
                            case 1:
                                {
                                    Console.WriteLine("Login");
                                    var login = Console.ReadLine();
                                    package = packageFactory.RegisterUserRequest(login);
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("Login");
                                    var login = Console.ReadLine();
                                    package = packageFactory.LoginUserRequest(login, string.Empty);
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Token");
                                    var token = Console.ReadLine();
                                    package = packageFactory.LogountUserRequest(token);
                                    package.SecurityToken = token;
                                    break;
                                }
                        }

                        Console.WriteLine("Отправляемый пакет: " + package.ToString());
                        sender.SendMessage(package, ip, remotePort);

                    }
                }
            }
        }
    }
}
