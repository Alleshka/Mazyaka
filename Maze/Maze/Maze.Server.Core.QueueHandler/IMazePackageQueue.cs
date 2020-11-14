using System;
using System.Net;
using Maze.Common.MazePackages;

namespace Maze.Server.Core.QueueHandler
{
    public interface IMazePackageQueue
    {
        /// <summary>
        /// Добавляет в очередь
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        void Add(IMazePackage package, IPEndPoint endPoint);

        /// <summary>
        /// Получает наиболее подходящий пакет
        /// </summary>
        /// <returns></returns>
        IMazePackage GetPackage(out IPEndPoint endPoint);

        Int32 Count { get; }
    }
}
