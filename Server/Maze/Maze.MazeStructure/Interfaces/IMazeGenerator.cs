namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeGenerator
    {
        public IMaze GenerateMaze(IMazeBuilder builder, int lineCount = 10, int colCount = 10);
    }
}
