using Maze.Common.MazePackages;
using System.Net;

namespace Maze.Server.Core.QueueHandler
{
    class SimpleMazePackageQueueItem : IMazePackageQueueItem
    {
        public IMazePackage MazePackage { get; set; }
        public IPEndPoint EndPoint { get; set; }

        public SimpleMazePackageQueueItem(IMazePackage mazePackage, IPEndPoint endPoint)
        {
            MazePackage = mazePackage;
            EndPoint = endPoint;
        }
    }
}
