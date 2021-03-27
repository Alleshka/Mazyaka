using Maze.Common.Model;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Maze.Common.MazePackages.MazePackageFactory")]
[assembly: InternalsVisibleTo("Maze.Server.MazeCommands.MazeCommandsFactory")]
[assembly: InternalsVisibleTo("Maze.Server.Core.Access")]
[assembly: InternalsVisibleTo("Maze.Common.MazePackages.Parsers")]

namespace Maze.Common.MazePackages.MazePackages
{
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


    internal abstract class BaseMazePackageRequest : BaseMazePackage, IMazePackageRequest
    {
        [JsonIgnore]
        public abstract MazeUserRole Roles { get; }
    }

    internal abstract class BaseMazePackageResponce : BaseMazePackage, IMazePackageResponce
    {

    }
}
