using Maze.Common;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMazeBuilder
    {
        public void BuildEmptyMaze(int rowCount, int colCount);

        public void BuildRoom(MazePoint point);
        public void BuildRoom(int row, int col);

        public void BuildWall(IMazeRoom room1, IMazeRoom room2);
        public void BuildWall(int row1, int col1, int row2, int col2);

        public void BuildNonDestroyableWall(IMazeRoom room, MoveDirection direction);
        public void BuildNonDestroyableWall(int row, int col, MoveDirection direction);

        public void BuildPassage(MazePoint point1, MazePoint point2);
        public void BuildPassage(int row1, int col1, int row2, int col2);
        public void BuildPassage(IMazeRoom room1, IMazeRoom room2);

        public void BuildExit(MazePoint point);
        public void BuildExit(int row, int col);

        public IMaze GetMaze();
    }
}
