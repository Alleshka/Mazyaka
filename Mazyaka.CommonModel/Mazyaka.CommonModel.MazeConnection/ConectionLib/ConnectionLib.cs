using System;
using System.Linq;
using System.Net.Sockets;

namespace Mazyaka.CommonModel.MazeConnection
{
    /// <summary>
    /// Отправляет и принимает пакеты данных
    /// </summary>
    public class ConnectionLib
    {
        private Socket client;

        public void ConnectServer(String ip, int port)
        {
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ip, port); // Подключаемся к серверу
        }

        /// <summary>
        /// Отключиться от сервера
        /// </summary>
        public void Disconnect()
        {
            client.Disconnect(true);
        }

        /// <summary>
        /// Отправляет сообщение серверу и не дожидается ответа
        /// </summary>
        /// <param name="command"></param>
        public void SendMessage(PackCommand command)
        {
            client.Send(PackCommand.ToBytes(command)); // Отправляем данные
        }

        /// <summary>
        /// Отправляет запрос к серверу и ожидает ответа
        /// </summary>
        /// <param name="command"></param>
        public PackCommand SendCommand(PackCommand command)
        {
            client.Send(PackCommand.ToBytes(command));
            byte[] bytes = new byte[1024];

            client.Receive(bytes);
            bytes = bytes.Where(x => x != 0).ToArray(); // Удаляем лишние нули

            return PackCommand.ToPack(bytes); // Возвращаем объект
        }
    }
}