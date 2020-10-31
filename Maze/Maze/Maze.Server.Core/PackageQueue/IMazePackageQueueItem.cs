using Maze.Common.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.PackageQueue
{
    internal interface IMazePackageQueueItem
    {
        IMazePackage MazePackage { get; set; }
    }

    internal class SimpleMazePackageQueueItem : IMazePackageQueueItem
    {
        public IMazePackage MazePackage { get; set; }

        public SimpleMazePackageQueueItem(IMazePackage mazePackage)
        {
            MazePackage = mazePackage;
        }
    }
}
