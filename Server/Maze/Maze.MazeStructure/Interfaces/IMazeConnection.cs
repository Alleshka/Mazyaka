using Maze.Common;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeConnection : IMazeSite
    {
        public bool CanDestroy { get; }
        public bool IsDestroyed { get; }

        public IMazeRoom this[MoveDirection direction] { get; }
        public IMazeRoom GetMazeSite(MoveDirection direction);
        public void SetMazeSite(MoveDirection direction, IMazeRoom site);

        public bool Destroy(MoveDirection direction);
    }
}
