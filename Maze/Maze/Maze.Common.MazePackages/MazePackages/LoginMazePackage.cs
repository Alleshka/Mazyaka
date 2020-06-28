using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет содержащий информацию для входа
    /// </summary>
    internal class LoginMazePackage : BaseMazePackage
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        public LoginMazePackage()
        {

        }

        public LoginMazePackage(string login, string password) : base()
        {
            this.Login = login;
            this.Password = password;
        }
    }
}
