using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.DevTools.CommandGenerator
{
    [Flags]
    public enum GenerateOptions
    {
        /// <summary>
        /// Генерирует два пакета, вместо одного. Один на заброс, один на ответ
        /// </summary>
        GenerateResponse = 0,
    }

    public class PackageGenerator
    {
    }
}
