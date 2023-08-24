using Maze.Common;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeBuilder
    {
        public void BuildEmptyMaze();

        public void BuildRoom(MazePoint point);
        public void BuildRoom(int line, int col);

        public void BuildPassage(MazePoint point1, MazePoint point2);
        public void BuildPassage(int line1, int col1, int line2, int col2);
        public void BuildPassage(IMazeRoom room1, IMazeRoom room2);

        public void BuildExit(MazePoint point);
        public void BuildExit(int line, int col);

        public IMaze GetMaze();
    }
}
