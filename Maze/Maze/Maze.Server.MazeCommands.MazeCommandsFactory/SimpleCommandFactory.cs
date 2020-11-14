using Maze.Common.MazePackages;
using Maze.Server.MazeCommands.MazeCommandsFactory.PackageHandlerChain;

namespace Maze.Server.MazeCommands.MazeCommandsFactory
{
    public class SimpleCommandFactory : IMazeCommandFactory
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
