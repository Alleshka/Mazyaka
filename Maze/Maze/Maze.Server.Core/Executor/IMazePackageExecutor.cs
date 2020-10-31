using Maze.Common.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Executor
{
    internal interface IMazePackageExecutor
    {
        void Execute(IMazePackage package);
    }
}
