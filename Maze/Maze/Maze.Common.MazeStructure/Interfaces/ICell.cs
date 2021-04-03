using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;

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

        void AddObject(IGameObject gameObject);
        void RemoveObject(ILiveGameObject gameObject);

        void Execute(ILiveGameObject gameObject);
    }
}
