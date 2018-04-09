using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using MazyakaMain.Server.Commands;

namespace MazyakaMain.Server.MazeServer
{
    public class MazeServer
    {
        private Socket socketListener;
        private bool IsServerWork;

        private ICommandParser commandParser;

        public MazeServer(int port = 1337)
        {
            socketListener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socketListener.Bind(new IPEndPoint(IPAddress.Any, port));
            socketListener.Listen(0);

        }

        public void Start()
        {
            IsServerWork = true;
            socketListener.BeginAccept(AcceptCallBack, null);
        }

        public void Stop()
        {
            IsServerWork = false;
            socketListener.Dispose();
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
                byte[] buffer = new byte[1024];
                client.Receive(buffer);

                ICommandAction command = commandParser.ParseCommand(buffer); // Получаем команду
                command.Execute(); // Запускаем выполнение
                command.SendResponse(); // Высылаем ответ, кому требуется
            }
        }
    }
}
