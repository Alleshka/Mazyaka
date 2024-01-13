using Maze.Common;
using Maze.Core;

namespace Maze.MazeStructure.Interfaces
{
    /// <summary>
    /// Interface for maze sites (e.g. door, wall, passage e.t.c.)
    /// </summary>
    public interface IMazeSite
    {
        public MoveResult Enter(IMazePlayer player, MoveDirection direction);

        public IMazeSite this[MoveDirection direction] { get; }
        public IMazeSite GetMazeSite(MoveDirection direction);

        public void SetMazeSite(MoveDirection direction, IMazeSite site);
    }
}
