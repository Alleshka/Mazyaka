using Maze.Common.MazePackages;
using Maze.Server.Commands;
using Maze.Server.Commands.CommandFactory.PackageHandlerChain;
using Maze.Server.Core.PackageHandlerChain;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.SessionStorage;

namespace Maze.Server.Core.CommandFactory
{
    public class SimpleCommandFactory
    {
        // Цепочка обязанностей
        private IMazePackageHandler _chain;

        protected ISessionStorage SessionStorage = DumpSessionStorage.Instance;
        protected IUserRepository UserRepository = new SimpleUserRepository();


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

            builder.AddChain(new LoginPackageHandler(SessionStorage, UserRepository));
            builder.AddChain(new CreateUserPachageHandler(SessionStorage, UserRepository));
            builder.AddChain(new LogoutPackageHandler(SessionStorage, UserRepository));
            builder.AddChain(new AccessDeniedPackageHandler(SessionStorage, UserRepository));

            return builder.Head;
        }
    }
}
