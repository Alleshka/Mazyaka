using Maze.MazeStructure.Metadata;

namespace Maze.MazeStructure
{
    public interface IMazeInfo
    {
        IMaze MazeStructure { get; }
        IMazeMetadata Metadata { get; }
    }
}
