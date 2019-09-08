
using MazeSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.LoginService
{
    interface ILoginService
    { 
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Информация о юзере</param>
        void SignUp(UserModel user);

        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserServiceModel SignIn(string login, string password);
    }
}
