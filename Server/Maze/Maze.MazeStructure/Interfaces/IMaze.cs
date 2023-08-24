using Maze.Common;

namespace Maze.MazeStructure.Interfaces
{
    public interface IMaze
    {
        public int LineCount { get; }
        public int ColCount { get; }

        public void AddRoom(IMazeRoom room);

        public IMazeRoom GetRoomByPoint(MazePoint point);
        public IMazeRoom GetRoomByCoordinates(int line, int col);
    }
}
