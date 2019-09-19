using System;

namespace MazeSolution.Core.Models
{
    /// <summary>
    /// Модель пользователя, которая хранится в базе
    /// </summary>
    public class UserModel : BaseMazeObject
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
