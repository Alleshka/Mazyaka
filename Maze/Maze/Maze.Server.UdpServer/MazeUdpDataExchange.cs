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
        private Action<string, IPEndPoint, MazeUdpDataExchange> _onReceiveMessage;

        private UdpClient _client = null;

        public MazeUdpDataExchange(int port, Action<string, IPEndPoint, MazeUdpDataExchange> onReceiveMessage = null)
        {
            _localPort = port;
            _onReceiveMessage = onReceiveMessage;
            _client = new UdpClient(_localPort);
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
        public void SendMessage(string message, IPEndPoint endPoint)
        {
            var data = Encoding.UTF8.GetBytes(message);
            _client.Send(data, data.Length, endPoint);
        }

        /// <summary>
        /// Отправка сообщений указанному адресату
        /// </summary>
        /// <param name="message">Отправляемое сообщение</param>
        /// <param name="ipAddress">ip адресата</param>
        /// <param name="port">Порт адресата</param>
        public void SendMessage(string message, string ipAddress, int port)
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
                LogMessage(data, remoteIp);


                /// Вызываем действия, срабатываемые при получении сообщения
                var message = Encoding.UTF8.GetString(data);
                _onReceiveMessage?.Invoke(message, remoteIp, this);
            }
        }

        protected virtual void LogMessage(byte[] data, IPEndPoint endPoint)
        {
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: Received Message ({data.Length} bytes)");
            foreach (var item in data)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine(System.Environment.NewLine);
        }

        public void Dispose()
        {
            _isServerStopped = true;
            _client.Dispose();
        }
    }
}
