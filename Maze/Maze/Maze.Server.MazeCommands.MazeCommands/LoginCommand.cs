using Maze.Common.MazePackages;
using Maze.Server.AutofacContainer;
using Maze.Server.MazeService.LoginService;
using Maze.Server.MazeService.SessionService;

namespace Maze.Server.MazeCommands.MazeCommands
{
    class LoginCommand : BaseCommand
    {
        private string _userLogin;

        public LoginCommand(string userLogin)
        {
            _userLogin = userLogin;
        }

        protected override IMazePackage ExecuteCommand()
        {
            var user = MazeAutofacContainer.Instance.GetService<ILoginService>().GetUserByLogin(_userLogin);
            if (user == null) return PackageFactory.ExceptionMessageResponse("Пользователь не найден");
            else
            {
                var token = MazeAutofacContainer.Instance.GetService<ISessionService>().AddUserSession(user);
                return PackageFactory.LoginUserResponse(token);
            }
        }
    }
}
