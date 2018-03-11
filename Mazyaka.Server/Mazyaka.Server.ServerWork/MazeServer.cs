using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Mazyaka.MazeGeneral;
using Mazyaka.Server.GameService;
using Mazyaka.Server.LoginService;
using Mazyaka.MazeGeneral.MazeModel;
using Mazyaka.MazeGeneral.GameModel;

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

                            Guid gameID = gameService.CreateGame(userID).RoomID; // Создаём игру
                            PackCommand pack = new PackCommand(TypeCommand.Responce);
                            pack.AddArgument(gameID.ToString());

                            client.Send(PackCommand.ToBytes(pack));
                            break;
                        }
                    case TypeCommand.JoinGame:
                        {
                            Guid.TryParse(command[0], out Guid gameID);
                            Guid.TryParse(command[1], out Guid userID);

                            bool status = gameService.JoinGame(gameID, userID);
                            PackCommand pack = new PackCommand(TypeCommand.Responce);
                            pack.AddArgument(status.ToString());

                            client.Send(PackCommand.ToBytes(pack));
                            break;
                        }
                    case TypeCommand.SendStartMaze:
                        {
                            Guid.TryParse(command[0], out Guid gameID);
                            Guid.TryParse(command[1], out Guid userID);
                            MazeArea area = MazeArea.ToObject(command[2]);

                            gameService.SendStartMaze(gameID, userID, area);

                            client.Send(PackCommand.ToBytes(new PackCommand(TypeCommand.Responce))); // Отправляем пустой ответ
                            break;
                        }
                    case TypeCommand.SendStartPoint:
                        {
                            Guid.TryParse(command[0], out Guid gameID);
                            Guid.TryParse(command[1], out Guid userID);
                            Point point = Point.ToObject(command[2]);

                            gameService.SendStartPoint(gameID, userID, point);

                            client.Send(PackCommand.ToBytes(new PackCommand(TypeCommand.Responce))); // Отправляем пустой ответ
                            break;
                        }
                    case TypeCommand.GetInitData:
                        {
                            Guid.TryParse(command[0], out Guid gameID);
                            Guid.TryParse(command[1], out Guid userID);

                            Player startInfo = gameService.GetInitData(gameID, userID);
                            PackCommand sendInfo = new PackCommand(TypeCommand.Responce);
                            sendInfo.AddArgument(Player.ToXML(startInfo));

                            client.Send(PackCommand.ToBytes(sendInfo));                        
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

                command = null;
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
