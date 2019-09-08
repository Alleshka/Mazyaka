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
        public UserLobbyModel (UserServiceModel serviceModel)
        {
            Login = serviceModel.Login;
            ObjectID = serviceModel.ObjectID;
            IsReady = false;
        }

        public string Login { get; }

        public bool IsReady { get; set; }
        // public int Level { get; }
    }
}
