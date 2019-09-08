using System;

namespace MazeSolution.Core.Models
{
    /// <summary>
    /// Модель пользователя, которая хранится в базе
    /// </summary>
    public class UserModel : BaseMazeObject
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
