using Maze.Common.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze.Common.MazaPackages
{
    /// <summary>
    /// Базовый интерфейс пакета для передачи через TCP
    /// </summary>
    public interface IMazePackage
    {
        string TypeName { get; }
    }


    /// <summary>
    /// Базовый класс пакета
    /// </summary>
    public abstract class BaseMazePackage : BaseMazeObject, IMazePackage
    {
        public BaseMazePackage()
        {

        }

        public string TypeName => (this.GetType().ToString()).Split('.').Last();
        public string SecurityToken { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
