using Maze.MazeStructure.Metadata;

namespace Maze.MazeStructure.MazeGenerators
{
    public interface IMazeGenerator
    {
        IMazeInfo Generate();
    }
}
