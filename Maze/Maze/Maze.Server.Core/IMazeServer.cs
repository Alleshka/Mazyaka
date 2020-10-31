using Maze.Server.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.Core
{
    /// <summary>
    /// Интерфейс сервера
    /// </summary>
    public interface IMazeServer
    {
        /// <summary>
        /// Запуск сервера с прослушиванием указанного порта
        /// </summary>
        /// <param name="port"></param>
        void Start(int port);

        /// <summary>
        /// Остановка сервера
        /// </summary>
        void Stop();
    }
}
