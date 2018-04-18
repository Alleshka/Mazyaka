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

        public delegate void NewRequestDelegate(String request);
        public event NewRequestDelegate NewRequest;
        public event Action NewResponse = delegate {};
        public event Action CliendDisconnect = delegate { };

        public MazeServer(int port = 1337)
        {
            socketListener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socketListener.Bind(new IPEndPoint(IPAddress.Any, port));
            socketListener.Listen(0);

            sender = MessageSender.Sender.GetInstanse();
            commandParser = new CommandParser();
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


                    ActAbstract command = commandParser.ParseCommand(buffer, client);
                    NewRequest?.Invoke(command.GetType().ToString()); // Событие нового сообщения
                    command.Execute();

                    NewResponse?.Invoke();
                }
                catch (SocketException)
                {
                    CliendDisconnect?.Invoke(); // Сообщем, что клиент отсоединился
                    break; // Выходим из цикла
                }
            }

            // Заканчиваем подключение
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            client.Dispose();
        }
    }
}
