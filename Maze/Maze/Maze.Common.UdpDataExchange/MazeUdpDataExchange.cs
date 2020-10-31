using Maze.Common.MazePackages;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Maze.Server.UdpServer
{
    /// <summary>
    /// Обменивается сообщениями с адресатом через протокол udp
    /// </summary>
    public class MazeUdpDataExchange : IDisposable
    {
        /// <summary>
        /// Локальный порт, который будет слушать текущий объект
        /// </summary>
        private readonly int _localPort;

        /// <summary>
        /// Признак остановки сервера
        /// </summary>
        private bool _isServerStopped;

        /// <summary>
        /// Срабатывает при получении сообщения
        /// </summary>
        private Action<IMazePackage, IPEndPoint, MazeUdpDataExchange> _onReceiveMessage;

        private UdpClient _client = null;

        private IMazePackageParser _packageParser;

        public MazeUdpDataExchange(int port, IMazePackageParser packageParser, Action<IMazePackage, IPEndPoint, MazeUdpDataExchange> onReceiveMessage = null)
        {
            _localPort = port;
            _onReceiveMessage = onReceiveMessage;
            _client = new UdpClient(_localPort);
            _packageParser = packageParser;
        }

        /// <summary>
        /// Запустить отдельный поток для прослушивания входящих сообщений
        /// </summary>
        public void Start()
        {
            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start();
        }

        /// <summary>
        /// Остановить прослушку входящих сообщений
        /// </summary>
        public void Stop()
        {
            _isServerStopped = true;
        }

        /// <summary>
        /// Отправка сообщений указанному адресату
        /// </summary>
        /// <param name="message">Отправляемое сообщение</param>
        /// <param name="endPoint">Адресат</param>
        public void SendMessage(IMazePackage message, IPEndPoint endPoint)
        {
            var data = _packageParser.GetBytes(message);
            _client.Send(data, data.Length, endPoint);
        }

        /// <summary>
        /// Отправка сообщений указанному адресату
        /// </summary>
        /// <param name="message">Отправляемое сообщение</param>
        /// <param name="ipAddress">ip адресата</param>
        /// <param name="port">Порт адресата</param>
        public void SendMessage(IMazePackage message, string ipAddress, int port)
        {
            SendMessage(message, new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        /// <summary>
        /// Обработчик прослушивания сообщений
        /// </summary>
        protected void ReceiveMessage()
        {
            while (!_isServerStopped)
            {
                IPEndPoint remoteIp = null;
                var data = _client.Receive(ref remoteIp);
                // LogMessage(data, remoteIp);

                /// Вызываем действия, срабатываемые при получении сообщения
                _onReceiveMessage?.Invoke(_packageParser.GetPackage(data), remoteIp, this);
            }
        }

        protected virtual void LogMessage(byte[] data, IPEndPoint endPoint)
        {
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Received Message ({data.Length} bytes)");
        }

        public void Dispose()
        {
            _isServerStopped = true;
            _client.Dispose();
        }
    }
}
