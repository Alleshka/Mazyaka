using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Mazyaka.MazeGeneral;
using Mazyaka.Server.GameService;
using Mazyaka.Server.LoginService;

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

        public MazeServer(int port = 1337)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp); // Создаём сокет для приёма подключения
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(0);
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
                    client.Receive(temp); // Принимаем сообщение
                }
                catch
                {
                    // Если клиент отключился
                    DisconnectClient(client);
                    return;
                }

                PackCommand command = PackCommand.ToPack(temp); // Достаём команду

                switch (command.Type)
                {
                    case TypeCommand.Login:
                        {
                            String login = command[0];
                            String password = command[1];

                            Guid id = loginService.Login(login, password);
                            PackCommand pack = new PackCommand(TypeCommand.Responce);
                            pack.AddArgument(id.ToString());

                            client.Send(PackCommand.ToBytes(pack));
                            break;
                        }
                    case TypeCommand.CreateGame:
                        {
                            Guid.TryParse(command[0], out Guid userID);

                            Guid gameID = gameService.CreateGame(userID); // Создаём игру
                            PackCommand pack = new PackCommand(TypeCommand.Responce);
                            pack.AddArgument(gameID.ToString());

                            client.Send(PackCommand.ToBytes(pack));
                            break;
                        }
                    case TypeCommand.JoinGame:
                        {
                            break;
                        }
                    case TypeCommand.SendStartMaze:
                        {
                            break;
                        }
                    case TypeCommand.SendStartPoint:
                        {
                            break;
                        }
                    case TypeCommand.GetInitData:
                        {
                            break;
                        }
                    default:
                        {
                            PackCommand pack = new PackCommand(TypeCommand.Error);
                            pack.AddArgument("Ошибка, неизвестная команда");

                            client.Send(PackCommand.ToBytes(pack));
                            break;
                        }
                }
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
