using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Mazyaka.MazeGeneral;
using Mazyaka.Server.GameService;
using Mazyaka.Server.LoginService;
using Mazyaka.MazeGeneral.MazeModel;
using Mazyaka.MazeGeneral.GameModel;
using System.Linq;

namespace Mazyaka.Server.ServerWork
{
    /// <summary>
    /// Работа сервера
    /// </summary>
    public class MazeServer
    {

        private Socket socket; // Принимает подключения
        private bool IsServerWork; // Флаг работы сервера

        private IGameService gameService;
        private ILoginService loginService;

        public MazeServer(IGameService game, ILoginService login, int port = 1337)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp); // Создаём сокет для приёма подключения
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(0);

            gameService = game;
            loginService = login;
        }

        /// <summary>
        /// Запуск сервера
        /// </summary>
        public void Start()
        {
            IsServerWork = true;
            socket.BeginAccept(AcceptCallBack, null);
        }

        public void Stop()
        {
            IsServerWork = false;
            socket.Dispose();
        }

        void AcceptCallBack(IAsyncResult result)
        {
            Socket client = socket.EndAccept(result); // Получаем входящий сокет
            Thread thread = new Thread(new ParameterizedThreadStart(HandleClient)); // Выделяем игроку свой поток
            thread.Start(client); // Запускаем поток

            socket.BeginAccept(AcceptCallBack, null); // Устанавливаем прослушивание заново
        }

        void HandleClient(object obj)
        {
            Socket client = obj as Socket;
            byte[] temp = new byte[2048]; // TODO: Узнать средний размер пакета в байтах выделять только под них
            while (IsServerWork)
            {
                try
                {
                    temp = new byte[2048];
                    client.Receive(temp); // Принимаем сообщение
                }
                catch
                {
                    // Если клиент отключился
                    DisconnectClient(client);
                    return;
                }
                
                // TODO : Реализация действия сервера
            }

            // После остановки сервера отключаем соединения
            DisconnectClient(client);
        }

        private void DisconnectClient(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            client.Dispose();;
        }
    }
}
