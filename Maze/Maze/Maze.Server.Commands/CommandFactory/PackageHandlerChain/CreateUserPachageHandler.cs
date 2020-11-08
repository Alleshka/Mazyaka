using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Commands.Commands;
using Maze.Server.Core.PackageHandlerChain;

namespace Maze.Server.Commands.CommandFactory.PackageHandlerChain
{
    class CreateUserPachageHandler : BasePackageHandler<RegisterUserPackage>
    {
        protected override IMazeServerCommand Handle(RegisterUserPackage package)
        {
            return new CreateUserCommand(package.UserLogin);
        }
    }
}
