using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.Core.Models;

namespace MazeSolution.LoginService
{
    public class SimpleLoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public SimpleLoginService(IUserRepository userRepository, IUserService userService)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public UserServiceModel SignIn(string login, string password)
        {
            var userModel = _userRepository.GetUser(login);
            if (IsPasswordEquals(password, userModel.Password))
            {
                return _userService.AddUser(userModel);
            }
            else return null;
        }

        public void SignUp(UserModel user)
        {
            user.ObjectID = Guid.NewGuid();
            _userRepository.SaveUser(user);
        }

        private bool IsPasswordEquals(string password1, string password2)
        {
            return password1.Equals(password2);
        }
    }
}
