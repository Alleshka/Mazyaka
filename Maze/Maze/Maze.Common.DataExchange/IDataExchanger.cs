using Maze.Common.MazePackages;
using System.Net;

namespace Maze.Common.DataExhange
{
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
    }
}
