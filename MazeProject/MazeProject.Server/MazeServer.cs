using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MazeProject.CommandBuilder;
using MazeProject.MessageSender;

namespace MazeProject.Server
{
    public class MazeServer
    {
        private Socket socketListener;
        private bool IsServerWork;

        private CommandParser commandParser;
        private MessageSender.MessageSender messageSender;

        public MazeServer(int port = 1337)
        {

            messageSender = new MessageSender.MessageSender();
            commandParser = new CommandParser(messageSender);

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
            Socket client = socketListener.EndAccept(result);
            Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
            thread.Start(client); // Запускаем поток
            socketListener.BeginAccept(AcceptCallBack, null);
        }

        void HandleClient(object obj)
        {
            Socket client = obj as Socket;

            while (IsServerWork)
            {
                try
                {
                    byte[] buffer = new byte[6500];
                    client.Receive(buffer);
                    System.Diagnostics.Trace.WriteLine("Приняли данные");

                    ICommand command = commandParser.Parse(buffer, client);

                    var packages = command.Execute();
                    foreach (var package in packages) messageSender.SendMessage(package.GetReceives(), package.ToString());
                }
                catch (SocketException)
                {
                    break;
                }
            }
            client.Shutdown(SocketShutdown.Both);
            client.Disconnect(false);
            client.Dispose();
        }
    }
}