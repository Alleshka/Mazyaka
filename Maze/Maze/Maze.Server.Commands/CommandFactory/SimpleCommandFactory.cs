using Maze.Common.MazePackages;
using Maze.Server.Commands;
using Maze.Server.Commands.CommandFactory.PackageHandlerChain;
using Maze.Server.Core.PackageHandlerChain;

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
            builder.AddChain(new CreateUserPachageHandler());
            builder.AddChain(new LogoutPackageHandler());
            builder.AddChain(new AccessDeniedPackageHandler());

            return builder.Head;
        }
    }
}
