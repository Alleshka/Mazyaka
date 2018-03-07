using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Mazyaka.Server.LoginService;
using Mazyaka.Server.GameService;
using Mazyaka.CommonModel.MazeConnection;
using Mazyaka.CommonModel.GameModel;

namespace Mazyaka.Server.MazeServerWork.Framework
{
    public class GameClient
    {
        public Guid playerID { get; set; }
        public Socket netClient { get; set; }
    }

    /// <summary>
    /// Работа сервера
    /// </summary>
    public class MazeServer
    {
        private Socket socket;
        public bool workServer;

        private ILoginService loginService;
        private IGameService gameService;

        public event Action EventConectClient = delegate { }; // Событие при подключении клиента
        public event Action EventAddCommand = delegate { }; // Событие при обработке команды клиента
        public event Action EventDisconnecntClient = delegate { }; // Событие при отклюении клиента

        List<GameClient> ClientList; // Список клиентов

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port">Слушаемый порт</param>
        /// <param name="login">Объект логинсервиса</param>
        public MazeServer(ILoginService login, IGameService game, int port = 1337)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp); // Создаём сокет для приёма подключения
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(0);

            loginService = login;
            gameService = game;

            ClientList = new List<GameClient>();
        }


        public void Start()
        {
            workServer = true;
            socket.BeginAccept(AcceptCallBack, null);
        }

        /// Закончить прослушивание
        public void Stop()
        {
            workServer = false;
        }

        /// <summary>
        /// Вызывается при подключении клиента
        /// </summary>
        /// <param name="result"></param>
        void AcceptCallBack(IAsyncResult result)
        {
            Socket client = socket.EndAccept(result); // Получаем входящий сокет
            Thread thread = new Thread(new ParameterizedThreadStart(HandleClient)); // Выделяем игроку свой поток
            thread.Start(client); // Запускаем поток

            EventConectClient();

            socket.BeginAccept(AcceptCallBack, null); // Устанавливаем прослушивание заново
        }

        void HandleClient(object obj)
        {
            Socket client = obj as Socket;
            byte[] temp = new byte[2048]; // TODO: Узнать средний размер пакета в байтах выделять только под них

            while (workServer)
            {
                try
                {
                    client.Receive(temp);
                }
                catch
                {
                    DisconnectClient(client);
                    return;
                }

                PackCommand command = PackCommand.ToPack(temp); // Достаём команду
                switch (command.type)
                {
                    // TODO: Всю реализацию вынести в отдельные методы
                    case TypeCommand.Login:
                        {
                            Guid id = loginService.Login(command.args[0], command.args[1]);

                            PackCommand answ = new PackCommand(TypeCommand.Response);
                            answ.AddArgument(id.ToString());

                            // Добавляем в список клиентов
                            ClientList.Add(new GameClient()
                            {
                                playerID = id,
                                netClient = client
                            });

                            client.Send(PackCommand.ToBytes(answ)); // Отправляем ответ
                            break;
                        }
                    case TypeCommand.Disconnect:
                        {
                            break;
                        }
                    case TypeCommand.CreateGame:
                        {
                            // Создаём комнату
                            Guid.TryParse(command.args[0], out Guid userID);
                            GameRoom room = gameService.CreateGame(userID);

                            PackCommand answ = new PackCommand(TypeCommand.Response);
                            answ.AddArgument(room.RoomID.ToString()); // Отправляем ID комнаты

                            client.Send(PackCommand.ToBytes(answ)); // Отправляем ответ

                            break;
                        }
                    case TypeCommand.JoinGame:
                        {
                            // Достаём данные
                            Guid.TryParse(command.args[0], out Guid gameID);
                            Guid.TryParse(command.args[1], out Guid userID);

                            bool status = gameService.JoinGame(gameID, userID); // Пытаемся присоединиться к комнате

                            // TODO: Пока допустим, что всегда хорошо
                            PackCommand answ = new PackCommand(TypeCommand.Response);
                            answ.AddArgument(gameID.ToString());

                            client.Send(PackCommand.ToBytes(answ));
                            break;
                        }
                    case TypeCommand.SendStartMaze:
                        {
                            Guid.TryParse(command.args[0], out Guid gameID);
                            Guid.TryParse(command.args[1], out Guid userId);
                            MazeArea maze = MazeArea.ToObject(command.args[2]);

                            gameService.SendMaze(gameID, userId, maze);

                            // Ответ не шлём
                            break;
                        }
                    case TypeCommand.MoveObject:
                        {
                            break;
                        }
                    case TypeCommand.SendStarPoint:
                        {
                            Guid.TryParse(command.args[0], out Guid gameID);
                            Guid.TryParse(command.args[1], out Guid userId);
                            Point point = Point.ToObject(command.args[2]);

                            gameService.SendStartPoint(gameID, userId, point);

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                EventAddCommand();
            }

            // После остановки сервера отключаем соединения
            DisconnectClient(client);
        }

        private void DisconnectClient(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            client.Dispose();
            EventDisconnecntClient();
        }
    }
}
