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

            server.NewRequest += Server_NewRequest;
            server.NewResponse += Server_NewResponse;

            server.Start();

            System.Console.WriteLine("Сервер работает");
            System.Console.WriteLine("Выход?");
            Console.ReadKey();
        }

        private static void Server_NewResponse(DateTime date, Guid Id)
        {
            Console.WriteLine($"[{date.ToString()}] Запрос {Id} обработан");
            Console.WriteLine();
        }

        private static void Server_NewRequest(DateTime date, string request, Guid id, Guid userID)
        {
            Console.WriteLine();
            Console.WriteLine($"[{date.ToString()}] Получен запрос {request} от пользователя с ID = {userID} (Запрос №{id}");
        }
    }
}
