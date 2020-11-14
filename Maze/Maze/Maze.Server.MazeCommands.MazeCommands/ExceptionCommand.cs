using Maze.Common.MazePackages;
using System;

namespace Maze.Server.MazeCommands.MazeCommands
{
    class ExceptionCommand : BaseCommand
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
