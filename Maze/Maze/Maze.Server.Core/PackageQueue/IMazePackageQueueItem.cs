using Maze.Common.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.PackageQueue
{
    public interface IMazePackageQueueItem
    {
        IMazePackage MazePackage { get; set; }
        IPEndPoint EndPoint { get; set; }
    }

    public class SimpleMazePackageQueueItem : IMazePackageQueueItem
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
