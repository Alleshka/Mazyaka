using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.Models
{
    /// <summary>
    /// Модель пользователя, представленная в лобби
    /// </summary>
    public class UserLobbyModel : BaseMazeObject
    {
        /// <param name="serviceModel">Модель сервиса пользователей</param>
        public UserLobbyModel (UserServiceModel serviceModel)
        {
            Login = serviceModel.Login;
            ObjectID = serviceModel.ObjectID;
            IsReady = false;
        }

        /// <summary>
        /// Логин игрока
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// Готовность игрока
        /// </summary>
        public bool IsReady { get; set; }
        // public int Level { get; }
    }
}
