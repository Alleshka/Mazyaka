using Maze.Common.MazePackages;
using System.Net;

namespace Maze.Server.Core.Executor
{
    internal interface IMazePackageExecutor
    {
        void Execute(IMazePackage package, IPEndPoint endPoint);
    }
}
