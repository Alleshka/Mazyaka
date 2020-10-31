using Maze.Common.MazePackages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    public class LoginCommand : BaseCommand
    {
        public LoginCommand(string login, string password)
        {

        }

        public override IMazePackage Execute()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Вы не допущены на бета-тест");
            return PackageFactory.HasNotAccessPackage();
        }
    }
}
