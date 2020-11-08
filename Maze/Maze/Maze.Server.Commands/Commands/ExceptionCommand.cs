using Maze.Common.MazePackages;
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

        public override IMazePackage Execute()
        {
            Console.WriteLine(_message);
            return PackageFactory.ExceptionMessageResponse(_message);
        }
    }
}
