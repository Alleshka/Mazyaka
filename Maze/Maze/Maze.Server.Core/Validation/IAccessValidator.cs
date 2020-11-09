using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using Maze.Common.Model;

namespace Maze.Server.Core.Validation
{
    interface IAccessValidator
    {
        IMazePackage Validate(IMazePackage package, MazeUserRole role);
    }
}
