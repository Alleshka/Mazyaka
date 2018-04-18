using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.Server.MazeServer;

namespace MazeProject.Server.GUIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server.MazeServer.MazeServer(1337);
            server.NewRequest += Printrequest;
            server.NewResponse += PrintResponse;
            server.CliendDisconnect += PrintDisconnect;

            server.Start();

            System.Console.WriteLine("Сервер работает");
            System.Console.WriteLine("Выход?");
            Console.ReadKey();
        }

        static void Printrequest(String msg)
        {
            Console.WriteLine("Получен новый запрос: " + msg);
        }

        static void PrintDisconnect()
        {
            Console.WriteLine("Клиент отключился");
        }

        static void PrintResponse()
        {
            Console.WriteLine("Обработано");
        }
    }
}
