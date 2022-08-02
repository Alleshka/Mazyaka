using Maze.Common.MazePackages;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Maze.Common.DataExhange
{
    /// <summary>
    /// Обменивается сообщениями с адресатом через протокол udp
    /// </summary>
    public class UdpDataExchanger : IDataExchanger, IDisposable
    {
        /// <summary>
        /// Локальный порт, который будет слушать текущий объект
        /// </summary>
        private int _localPort;

        /// <summary>
        /// Признак остановки сервера
        /// </summary>
        private bool _isServerStopped;

        /// <summary>
        /// Парсер пакетов
        /// </summary>
        private IMazePackageParser _packageParser;

        private UdpClient _udpClient;

        /// <summary>
        /// <see cref="IDataExchanger.OnRecieveMessage"/>
        /// </summary>
        public event DataExchangerHandler OnRecieveMessage;

        protected UdpClient UdpClient
        {
            get
            {
                return (_udpClient ??= new UdpClient(_localPort));
            }
        }

        public UdpDataExchanger(IMazePackageParser packageParser)
        {
            _packageParser = packageParser;
        }

        /// <summary>
        /// <see cref="IDataExchanger.Start(int)"/>
        /// </summary>
        /// <param name="port"></param>
        public void Start(int port)
        {
            _localPort = port;
            Task.Run(ReceiveMessage);
        }

        /// <summary>
        /// <see cref="IDataExchanger.Stop"/>
        /// </summary>
        public void Stop()
        {
            _isServerStopped = true;

            _udpClient.Dispose();
            _udpClient = null;
        }

        /// <summary>
        /// <see cref="IDataExchanger.SendMessage(IMazePackage, IPEndPoint)"/>
        /// </summary>
        public void SendMessage(IMazePackage message, IPEndPoint endPoint)
        {
            var data = _packageParser.GetBytes(message);
            UdpClient.Send(data, data.Length, endPoint);
        }

        /// <summary>
        /// <see cref="IDataExchanger.SendMessage(IMazePackage, string, int)"/>
        /// </summary>
        public void SendMessage(IMazePackage message, string ipAddress, int port)
        {
            SendMessage(message, new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        /// <summary>
        /// <see cref="SendMessageAsync(IMazePackage, IPEndPoint)"/>
        /// </summary>
        public async Task SendMessageAsync(IMazePackage message, IPEndPoint endPoint)
        {
            var data = _packageParser.GetBytes(message);
            await _udpClient.SendAsync(data, data.Length, endPoint);
        }

        /// <summary>
        /// <see cref="IDataExchanger.SendMessageAsync(IMazePackage, string, int)"/>
        /// </summary>
        public async Task SendMessageAsync(IMazePackage message, string ipAddress, int port)
        {
            await SendMessageAsync(message, new IPEndPoint(IPAddress.Parse(ipAddress), port));
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
