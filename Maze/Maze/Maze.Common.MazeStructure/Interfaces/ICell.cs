using Maze.Common.MazeStructure.Directions;

namespace Maze.Common.MazeStructure
{
    /// <summary>
    /// Ячейка лабиринта
    /// </summary>
    public interface ICell : IMazeBlock
    {
        IMazeBlock GetBlock(IMazeDirection direction);
        void SetBlock(IMazeDirection direction, IMazeBlock block);

        IMazeBlock this[IMazeDirection direction] { get; }
    }
}
