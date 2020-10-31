using Maze.Common.MazePackages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
        void Add(IMazePackage package);

        /// <summary>
        /// Получает наиболее подходящий пакет
        /// </summary>
        /// <returns></returns>
        IMazePackage GetPackage();

        Int32 Count { get; }
    }

    internal class SimpleMazePackageQueue : IMazePackageQueue
    {
        private ConcurrentQueue<IMazePackageQueueItem> _mazePackageQueueItems;

        public SimpleMazePackageQueue()
        {
            _mazePackageQueueItems = new ConcurrentQueue<IMazePackageQueueItem>();
        }

        public int Count => _mazePackageQueueItems.Count;

        public void Add(IMazePackage package)
        {
            _mazePackageQueueItems.Enqueue(new SimpleMazePackageQueueItem(package));
        }

        public IMazePackage GetPackage()
        {
            if (Count > 0)
            {
                IMazePackageQueueItem result = null;
                while (!_mazePackageQueueItems.TryDequeue(out result)) 
                {
                    if (Count == 0 && result == null) return null;
                };
                return result.MazePackage;
            }
            else return null;
        }
    }
}
