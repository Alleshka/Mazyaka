using Maze.Common;
using Maze.Core;

namespace Maze.MazeStructure.Interfaces
{
    /// <summary>
    /// Interface for maze sites (e.g. door, wall, passage e.t.c.)
    /// </summary>
    public interface IMazeSite
    {
        public MoveResult Enter(IMazePlayer player);
    }
}
