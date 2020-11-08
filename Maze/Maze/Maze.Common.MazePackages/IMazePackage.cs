using Maze.Common.Model;
using Newtonsoft.Json;
using System.Linq;

namespace Maze.Common.MazePackages
{
    /// <summary>
    /// Базовый интерфейс пакета для передачи через TCP
    /// </summary>
    public interface IMazePackage
    {
        string TypeName { get; }
        string SecurityToken { get; set; }
    }

    /// <summary>
    /// Базовый класс пакета
    /// </summary>
    internal abstract class BaseMazePackage : BaseMazeObject, IMazePackage
    {
        protected internal BaseMazePackage()
        {
        }

        public string TypeName => this.GetType().Name;
        public string SecurityToken { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
