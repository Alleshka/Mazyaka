using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazyaka.Server.MazeServerWork.Framework;
using Mazyaka.Server.GameService;
using Mazyaka.Server.LoginService;

namespace Mazyaka.Server.UI.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Инициализация сервера...");
            MazeServer mazeServer = new MazeServer(new TestLoginService(), new TestGameService());

            mazeServer.EventAddCommand += MazeServer_PrintCommand;
            mazeServer.EventConectClient += MazeServer_ConnectClient;
            mazeServer.EventDisconnecntClient += MazeServer_EventDisconnecntClient;


            Console.WriteLine("Успешно");
            Console.WriteLine("Запуск сервера...");

            mazeServer.Start();

            Console.WriteLine("Сервер запущен");
            Console.WriteLine(Environment.NewLine + "=============================================================================" + Environment.NewLine);

            while (true)
            {
                Console.WriteLine("Ожидаю подключения...");
                System.Threading.Thread.Sleep(10000);
                if (mazeServer.workServer == false) break;
            }

        }

        private static void MazeServer_EventDisconnecntClient()
        {
            Console.WriteLine("клиент отключён");
        }

        private static void MazeServer_ConnectClient()
        {
            Console.WriteLine("Подключён новый клиент");
        }

        private static void MazeServer_PrintCommand()
        {
            Console.WriteLine("Принят пакет от клиента");
        }
    }
}
