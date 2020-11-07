using Maze.Common.MazePackages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        IMazePackageQueueItem GetItem();

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

        public IMazePackageQueueItem GetItem()
        {
            if (Count > 0)
            {
                IMazePackageQueueItem result = null;
                while (!_mazePackageQueueItems.TryDequeue(out result)) 
                {
                    if (Count == 0 && result == null) return null;
                };
                return result;
            }
            else return null;
        }
    }
}
