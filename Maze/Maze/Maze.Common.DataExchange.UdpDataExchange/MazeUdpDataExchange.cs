using Maze.Common.DataExhange;
using Maze.Common.MazePackages;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Maze.Server.UdpServer
{
    /// <summary>
    /// Обменивается сообщениями с адресатом через протокол udp
    /// </summary>
    public class MazeUdpDataExchange : IDataExchanger, IDisposable
    {
        /// <summary>
        /// Локальный порт, который будет слушать текущий объект
        /// </summary>
        private int _localPort;

        /// <summary>
        /// Признак остановки сервера
        /// </summary>
        private bool _isServerStopped;

        private IMazePackageParser _packageParser;

        private UdpClient _udpClient;

        public event DataExchangerHandler OnRecieveMessage;

        protected UdpClient UdpClient
        {
            get
            {
                return (_udpClient ?? (_udpClient = new UdpClient(_localPort)));
            }
        }

        public MazeUdpDataExchange(IMazePackageParser packageParser)
        {
            _packageParser = packageParser;
        }

        /// <summary>
        /// Запустить отдельный поток для прослушивания входящих сообщений
        /// </summary>
        public void Start(int port)
        {
            _localPort = port;

            Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start();
        }

        /// <summary>
        /// Остановить прослушку входящих сообщений
        /// </summary>
        public void Stop()
        {
            _isServerStopped = true;

            _udpClient.Dispose();
            _udpClient = null;
        }

        /// <summary>
        /// Отправка сообщений указанному адресату
        /// </summary>
        /// <param name="message">Отправляемое сообщение</param>
        /// <param name="endPoint">Адресат</param>
        public void SendMessage(IMazePackage message, IPEndPoint endPoint)
        {
            var data = _packageParser.GetBytes(message);
            UdpClient.Send(data, data.Length, endPoint);
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
                var data = UdpClient.Receive(ref remoteIp);
                LogMessage(data, remoteIp);

                /// Вызываем действия, срабатываемые при получении сообщения
                OnRecieveMessage?.Invoke(this, new DataExchengerEventArgs(_packageParser.GetPackage(data), remoteIp));
            }
        }

        protected virtual void LogMessage(byte[] data, IPEndPoint endPoint)
        {
            // Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Received Message ({data.Length} bytes)");
        }

        public void Dispose()
        {
            _isServerStopped = true;
            UdpClient.Dispose();
        }
    }
}
