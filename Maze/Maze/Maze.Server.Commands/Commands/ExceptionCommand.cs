using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    public class ExceptionCommand : BaseCommand
    {
        private string _message;
        public ExceptionCommand(string message)
        {
            _message = message;
        }

        protected override IMazePackage ExecuteCommand()
        {
            Console.WriteLine(_message);
            return PackageFactory.ExceptionMessageResponse(_message);
        }
    }
}
