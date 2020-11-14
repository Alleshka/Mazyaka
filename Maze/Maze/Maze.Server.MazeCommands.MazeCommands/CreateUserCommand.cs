using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.MazeService.LoginService;
using Maze.Server.MazeService.SessionService;
using Maze.Server.ServiceStorage;

namespace Maze.Server.MazeCommands.MazeCommands
{
    class CreateUserCommand : BaseCommand
    {
        private string _userLogin;

        public CreateUserCommand(string userLogin)
        {
            _userLogin = userLogin;
        }

        protected override IMazePackage ExecuteCommand()
        {
            var user = new MazeUser()
            {
                Login = _userLogin,
                Role = new MazeUserRole(Constants.Roles.PLAYER)
            };

            MazeServiceStorage.Instance.GetService<ILoginService>().CreateUser(user);
            var token = MazeServiceStorage.Instance.GetService<ISessionService>().AddUserSession(user);

            return PackageFactory.LoginUserResponse(token);
        }
    }
}
