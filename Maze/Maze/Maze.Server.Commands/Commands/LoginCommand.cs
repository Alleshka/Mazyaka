using Maze.Common.MazePackages;
using Maze.Common.Model;
using Maze.Server.Core.Repositories;
using Maze.Server.Core.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    class LoginCommand : BaseCommand
    {
        private ISessionStorage _sessionStorage;
        private IUserRepository _userRepository;

        private string _userLogin;


        public LoginCommand(ISessionStorage sessionStorage, IUserRepository userRepository, string userLogin)
        {
            _sessionStorage = sessionStorage;
            _userRepository = userRepository;
            _userLogin = userLogin;
        }

        public override IMazePackage Execute()
        {
            var user = _userRepository.GetUserByLogin(_userLogin);
            if (user == null) return PackageFactory.ExceptionPackage("Пользователь не найден");
            else
            {
                var token = _sessionStorage.AddUserSession(user);
                return PackageFactory.LoginAnswerPackage(token);
            }
        }
    }
}
