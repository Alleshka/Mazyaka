using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using System.Net;

namespace Maze.Server.Core.Executor
{
    internal interface IMazePackageExecutor
    {
        void Execute(IMazePackage package, IPEndPoint endPoint);
    }
}
