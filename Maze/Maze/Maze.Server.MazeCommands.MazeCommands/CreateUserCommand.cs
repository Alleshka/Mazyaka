using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.AutofacContainer;
using Maze.Server.MazeService.LoginService;
using Maze.Server.MazeService.SessionService;


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
                Role = MazeUserRole.Player
            };

            MazeAutofacContainer.Instance.GetService<ILoginService>().CreateUser(user);
            var token = MazeAutofacContainer.Instance.GetService<ISessionService>().AddUserSession(user);

            return PackageFactory.LoginUserResponse(token);
        }
    }
}
