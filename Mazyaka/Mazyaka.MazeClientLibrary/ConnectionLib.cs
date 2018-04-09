﻿using System;
using System.Linq;
using System.Net.Sockets;
using Mazyaka.MazeGeneral;

namespace Mazyaka.MazeClientLibrary
{
    /// <summary>
    /// Отправляет и принимает пакеты данных
    /// </summary>
    public class Connection
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

        public PackCommand WaitMessage()
        {
            byte[] bytes = new byte[1024];
            client.Receive(bytes); // Принимаем сообщение

            bytes = bytes.Where(x => x != 0).ToArray(); // Удаляем лишние нули

            return PackCommand.ToPack(bytes); // Возвращаем объект
        }

        /// <summary>
        /// Отправляет запрос к серверу и ожидает ответа
        /// </summary>
        /// <param name="command"></param>
        public PackCommand SendCommand(PackCommand command)
        {
            SendMessage(command);
            return WaitMessage();
        }
    }
}