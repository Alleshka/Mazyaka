using Maze.Common.Model;

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

    public interface IMazePackageRequest : IMazePackage
    {
        MazeUserRole Roles { get; }
    }

    public interface IMazePackageResponce : IMazePackage
    {

    }
}
