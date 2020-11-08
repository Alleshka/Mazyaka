using Maze.Common;
using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    class CreateUserCommand : BaseCommand
    {
        private IUserService _userRepository;
        private ISessionStorage _sessionStorage;
        private string _userLogin;

        public CreateUserCommand(ISessionStorage sessionStorage, IUserService userRepository, string userLogin)
        {
            _userRepository = userRepository;
            _sessionStorage = sessionStorage;
            _userLogin = userLogin;
        }

        public override IMazePackage Execute()
        {
            var user = new MazeUser()
            {
                Login = _userLogin,
                Role = new MazeUserRole(Constants.Roles.PLAYER)
            };

            _userRepository.CreateUser(user);
            var token = _sessionStorage.AddUserSession(user);

            return PackageFactory.LoginUserResponse(token);
        }
    }
}
