using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace Maze.Server.Core.PackageQueue
{
    internal interface IMazePackageQueue
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

    public class SimpleMazePackageQueue : IMazePackageQueue
    {
        private ConcurrentQueue<IMazePackageQueueItem> _mazePackageQueueItems;

        public SimpleMazePackageQueue()
        {
            _mazePackageQueueItems = new ConcurrentQueue<IMazePackageQueueItem>();
        }

        public int Count => _mazePackageQueueItems.Count;

        public void Add(IMazePackage package, IPEndPoint endPoint)
        {
            _mazePackageQueueItems.Enqueue(new SimpleMazePackageQueueItem(package, endPoint));
        }

        public IMazePackage GetPackage(out IPEndPoint endPoint)
        {
            endPoint = null;

            if (Count > 0)
            {
                IMazePackageQueueItem result;
                while (!_mazePackageQueueItems.TryDequeue(out result))
                {
                    if (Count == 0 && result == null) return null;
                };

                endPoint = result.EndPoint;
                return result.MazePackage;
            }
            else
            {
                return null;
            }
        }
    }
}
