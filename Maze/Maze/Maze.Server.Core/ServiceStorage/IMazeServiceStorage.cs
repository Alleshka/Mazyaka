using Maze.Server.MazeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.ServiceStorage
{
    interface IMazeServiceStorage
    {
        void Register(Type serviceType, IMazeService actualService);
    }
}
