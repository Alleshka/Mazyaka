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

namespace Mazyaka.Server.MazeServerWork.Framework
{
    /// <summary>
    /// Работа сервера
    /// </summary>
    public class MazeServer
    {
        private Socket socket;
        private bool workServer;

        private ILoginService loginService;
        private IGameService gameService;

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
            //socket.Disconnect(true);
            //socket.Dispose();
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

            socket.BeginAccept(AcceptCallBack, null); // Устанавливаем прослушивание заново
        }

        void HandleClient(object obj)
        {
            Socket client = obj as Socket;
            byte[] temp = new byte[2048]; // TODO: Узнать средний размер пакета в байтах выделять только под них

            while (workServer)
            {
                client.Receive(temp);
                PackCommand command = PackCommand.ToPack(temp); // Достаём команду

                switch (command.type)
                {
                    // TODO: Всю реализацию вынести в отдельные методы
                    case TypeCommand.Login:
                        {
                            Guid id = loginService.Login(command.args[0], command.args[1]);

                            PackCommand answ = new PackCommand(TypeCommand.Response);
                            answ.AddArgument(id.ToString());

                            client.Send(PackCommand.ToBytes(answ)); // Отправляем ответ
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
                            //if (status == true)
                            //{
                                PackCommand answ = new PackCommand(TypeCommand.Response);
                                answ.AddArgument(gameID.ToString()); // Отправляем ID комнаты
                            //}

                            break;
                        }
                    case TypeCommand.IsGameStart:
                        {
                            break;
                        }
                    case TypeCommand.Disconnect:
                        {
                            break;
                        }
                    case TypeCommand.IsMyStep:
                        {
                            break;
                        }
                    case TypeCommand.MoveObject:
                        {
                            break;
                        }
                    case TypeCommand.SendMaze:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            // После остановки сервера отключаем соединение
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            client.Dispose();
        }
    }
}
