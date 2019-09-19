using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.Core.Models;

namespace MazeSolution.LoginService
{
    /// <summary>
    /// Базовая реализация сервиса авторизации
    /// </summary>
    public class SimpleLoginService : ILoginService
    {
        /// <summary>
        /// Репозиторий пользователей
        /// </summary>
        private readonly IUserRepository _userRepository;
        
        /// <summary>
        /// Сервис пользовательских сессий
        /// </summary>
        private readonly IUserService _userService;

        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="userService">Сервис пользовательских сессий/param>
        public SimpleLoginService(IUserRepository userRepository, IUserService userService)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Сессию пользователя</returns>
        public UserServiceModel SignIn(string login, string password)
        {
            var userModel = _userRepository.GetUser(login);
            if (IsPasswordEquals(password, userModel.Password))
            {
                return _userService.AddUser(userModel);
            }
            else return null;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Информация о юзере</param>
        public void SignUp(UserModel user)
        {
            user.ObjectID = Guid.NewGuid();
            _userRepository.SaveUser(user);
        }

        /// <summary>
        /// Сравнение паролей
        /// </summary>
        /// <param name="password1">Пароль 1</param>
        /// <param name="password2">Пароль 2</param>
        /// <returns>True если пароли совпадают, иначе false</returns>
        private bool IsPasswordEquals(string password1, string password2)
        {
            return password1.Equals(password2);
        }
    }
}
