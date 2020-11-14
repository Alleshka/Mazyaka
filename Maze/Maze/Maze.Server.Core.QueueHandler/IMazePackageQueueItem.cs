using Maze.Common.MazePackages;
using System.Net;

namespace Maze.Server.Core.QueueHandler
{
    interface IMazePackageQueueItem
    {
        IMazePackage MazePackage { get; set; }
        IPEndPoint EndPoint { get; set; }
    }
}
