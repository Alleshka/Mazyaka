using Maze.Common.Model;

namespace Maze.Common.MazePackages
{
    public interface IMazePackage
    {
        string TypeName { get; }
        string SecurityToken { get; set; }
    }

    public interface IMazePackageRequest : IMazePackage
    {
        MazeUserRole Roles { get; }
    }

    public interface IMazePackageResponse : IMazePackage
    {

    }
}
