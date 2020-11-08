using Maze.Common.MazePackages;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionService;

namespace Maze.Server.Commands.Commands
{
    class LoginCommand : BaseCommand
    {
        private string _userLogin;

        public LoginCommand(string userLogin)
        {
            _userLogin = userLogin;
        }

        public override IMazePackage Execute()
        {
            var user = MazeServiceStorage.Instance.GetService<IUserService>().GetUserByLogin(_userLogin);
            if (user == null) return PackageFactory.ExceptionMessageResponse("Пользователь не найден");
            else
            {
                var token = MazeServiceStorage.Instance.GetService<ISessionService>().AddUserSession(user);
                return PackageFactory.LoginUserResponse(token);
            }
        }
    }
}
