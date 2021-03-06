﻿using Maze.Common.Model;
using System;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет содержащий информацию для входа
    /// </summary>
    internal class LoginMazePackage : BaseMazePackageRequest
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        public override MazeUserRole Roles => MazeUserRole.All;

        public LoginMazePackage()
        {

        }

        public LoginMazePackage(string login, string password) : base()
        {
            this.Login = login;
            this.Password = password;
        }
    }

    internal class LoginResponceMazePackage : BaseMazePackage
    {
        public String UserToken { get; set; }
        public Guid UserID { get; set; }
    }
}
