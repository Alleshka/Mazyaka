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
        public void Start(int port);

        /// <summary>
        /// Остановка сервера
        /// </summary>
        public void Stop();

        /// <summary>
        /// Добавить команду в очередь
        /// </summary>
        /// <param name="command"></param>
        public void AddCommand(IMazeServerCommand command);
    }
}
