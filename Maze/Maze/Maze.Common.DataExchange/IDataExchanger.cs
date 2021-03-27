using Maze.Common.MazePackages;
using System;
using System.Net;

namespace Maze.Common.DataExhange
{
    public class DataExchengerEventArgs : EventArgs
    {
        private IMazePackage _mazePackage;
        private IPEndPoint _iPEndPoint;

        public IMazePackage Package => _mazePackage;
        public IPEndPoint IPEndPoint => _iPEndPoint;

        public DataExchengerEventArgs(IMazePackage package, IPEndPoint iPEndPoint)
        {
            _mazePackage = package;
            _iPEndPoint = iPEndPoint;
        }
    }


    public delegate void DataExchangerHandler(IDataExchanger sender, DataExchengerEventArgs args);

    public interface IDataExchanger
    {
        /// <summary>
        /// Запускает прослушку указанного порта
        /// </summary>
        /// <param name="port"></param>
        void Start(int port);

        /// <summary>
        /// Останавливает прослушку
        /// </summary>
        void Stop();

        /// <summary>
        /// Отправляет указанный пакет получателю
        /// </summary>
        /// <param name="message"></param>
        /// <param name="endPoint"></param>
        void SendMessage(IMazePackage message, IPEndPoint endPoint);

        /// <summary>
        /// Отправляет указанный пакет на указанный ip и порт
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        void SendMessage(IMazePackage message, string ipAddress, int port);

        /// <summary>
        /// Событие, срабатывающее при получении сообщения
        /// </summary>
        event DataExchangerHandler OnRecieveMessage;
    }
}
