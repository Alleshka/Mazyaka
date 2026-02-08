using Maze.Common;

namespace Maze.MazeStructure.MazeSites
{
    public interface IMazeRoom
    {
        public int Id { get; }

        public void AddConnection(MoveDirection direction, IMazeConnection connection);
        public IMazeConnection? GetConnection(MoveDirection direction);
    }
}
