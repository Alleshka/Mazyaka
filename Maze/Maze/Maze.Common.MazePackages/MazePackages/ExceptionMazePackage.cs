using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazePackages.MazePackages
{
    /// <summary>
    /// Пакет с сообщением об исключении
    /// </summary>
    internal class ExceptionMazePackage : BaseMazePackage
    {
        public String Message { get; set; }

        public ExceptionMazePackage()
        {

        }

        public ExceptionMazePackage(string message)
        {
            Message = message;
        }
    }
}
