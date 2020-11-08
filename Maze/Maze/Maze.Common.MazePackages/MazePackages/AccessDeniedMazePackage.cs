using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет с сообщением о запрете доступа
    /// </summary>
    internal class AccessDeniedMazePackage : BaseMazePackage
    {
        public string Message { get; set; } = "Доступ запрещён";
    }
}
