using Maze.Common.MazePackages;
using System.Net;

namespace Maze.Server.Core.QueueHandler
{
    public interface IMazePackageQueueHandler
    {
        void Start();
        void Stop();

        void AddPackage(IMazePackage mazePackage, IPEndPoint endPoint);
    }
}
