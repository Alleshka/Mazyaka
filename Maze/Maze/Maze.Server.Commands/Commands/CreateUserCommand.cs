using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionStorage;

namespace Maze.Server.Commands.Commands
{
    class CreateUserCommand : BaseCommand
    {
        private string _userLogin;

        public CreateUserCommand(string userLogin)
        {
            _userLogin = userLogin;
        }

        public override IMazePackage Execute()
        {
            var user = new MazeUser()
            {
                Login = _userLogin,
                Role = new MazeUserRole(Constants.Roles.PLAYER)
            };

            MazeServiceStorage.Instance.GetService<IUserService>().CreateUser(user);
            var token = MazeServiceStorage.Instance.GetService<ISessionStorage>().AddUserSession(user);

            return PackageFactory.LoginUserResponse(token);
        }
    }
}
