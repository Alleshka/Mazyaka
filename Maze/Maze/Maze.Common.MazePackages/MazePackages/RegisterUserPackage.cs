using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет регистрации пользователей
    /// </summary>
    internal class RegisterUserPackage : BaseMazePackage
    {
        public string UserLogin { get; set; }
    }

    /// <summary>
    /// Пакет с ответом регистрации 
    /// </summary>
    internal class RegisterUserResponePackage : BaseMazePackage
    {
        public MazeUser MazeUser { get; set; }
    }
}
