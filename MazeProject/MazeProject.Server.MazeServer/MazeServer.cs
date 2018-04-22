using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

using MazeProject.Server.CommandBuilder;
using MazeProject.Server.CommandBuilder.CommandAction;
using MazeProject.Server.MessageSender;

namespace MazeProject.Server.MazeServer
{
    public class MazeServer
    {
        private Socket socketListener;
        private bool IsServerWork;

        private ICommandParser commandParser;
        private Sender sender;

        public delegate void NewRequestDelegate(DateTime date, String request, Guid commandID, Guid userID);
        public event NewRequestDelegate NewRequest;

        public delegate void NewResponseDelegate(DateTime date, Guid Id);
        public event NewResponseDelegate NewResponse;

        public event Action CliendDisconnect = delegate { };

        public MazeServer(int port = 1337)
        {
            socketListener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socketListener.Bind(new IPEndPoint(IPAddress.Any, port));
            socketListener.Listen(0);

            sender = new Sender();
            commandParser = new CommandParser(sender);
        }

        public void Start()
        {
            IsServerWork = true;
            socketListener.BeginAccept(AcceptCallBack, null);
        }

        public void Stop()
        {
            IsServerWork = false;
            sender.Clear();
            socketListener.Dispose(); // TODO: Переделать отключение сервера
        }

        void AcceptCallBack(IAsyncResult result)
        {
            Socket client = socketListener.EndAccept(result); // Получаем входящий сокет
            Thread thread = new Thread(new ParameterizedThreadStart(HandleClient)); // Выделяем игроку свой поток
            thread.Start(client); // Запускаем поток

            socketListener.BeginAccept(AcceptCallBack, null); // Устанавливаем прослушивание заново
        }

        void HandleClient(object obj)
        {
            Socket client = obj as Socket;

            while (IsServerWork)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    client.Receive(buffer); // Принимаем данные

                    Guid id = Guid.NewGuid();

                    ActAbstract command = commandParser.ParseCommand(buffer, client);
                    
                    if(sender.ExistUser(client)) NewRequest?.Invoke(DateTime.Now, command.ToString().Split('.').Last(), id, sender.GetUserID(client)); // Событие нового сообщения
                    else NewRequest?.Invoke(DateTime.Now, command.ToString().Split('.').Last(), id, Guid.Empty); // Событие нового сообщения

                    command.Execute();
                    NewResponse?.Invoke(DateTime.Now, id);
                }
                catch (SocketException)
                {
                    break; // Выходим из цикла
                }
            }

            // Заканчиваем подключение
            sender.RemoveUser(client);
            CliendDisconnect.Invoke();
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            client.Dispose();
        }
    }
}
