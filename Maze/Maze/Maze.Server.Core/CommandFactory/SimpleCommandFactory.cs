using Maze.Common.MazePackages;
using Maze.Server.Commands;
using Maze.Server.Core.PackageHandlerChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.CommandFactory
{
    public class SimpleCommandFactory
    {
        // Цепочка обязанностей
        private IMazePackageHandler _chain;

        public SimpleCommandFactory()
        {
            _chain = CreateChain();
        }

        public IMazeServerCommand CreateCommand(IMazePackage mazePackage)
        {
            return _chain.HandlePackage(mazePackage);
        }

        private IMazePackageHandler CreateChain()
        {
            var builder = new PackageChainHandlerBuilder();
            builder.AddChain(new LoginPackageHandler());
            return builder.Head;
        }
    }
}
