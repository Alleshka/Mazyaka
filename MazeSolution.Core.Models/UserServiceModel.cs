using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.Models
{
    /// <summary>
    /// Модель пользователя, описывающая его сессию
    /// </summary>
    public class UserServiceModel : BaseMazeObject
    {
        public UserServiceModel()
        {
            SecurityToken = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Токен доступа
        /// </summary>
        public string SecurityToken { get; }
    }
}
