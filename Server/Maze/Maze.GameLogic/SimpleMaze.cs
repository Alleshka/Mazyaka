using Maze.Common;
using Maze.MazeStructure.Interfaces;
using Maze.MazeStructure.MazeSites;

namespace Maze.MazeStructure
{
    internal class SimpleMaze : IMaze
    {
        private Dictionary<MazePoint, IMazeRoom> _rooms;

        public int RowCount { get; protected set; }

        public int ColCount { get; protected set; }

        public SimpleMaze(int rowCount, int colCount)
        {
            _rooms = new Dictionary<MazePoint, IMazeRoom>();

            RowCount = rowCount;
            ColCount = colCount;
        }

        public void AddRoom(IMazeRoom room)
        {
            _rooms.Add(room.Point, room);
        }

        public IMazeRoom GetRoomByCoordinates(int line, int col)
        {
            var point = new MazePoint(line, col);
            return GetRoomByPoint(point);
        }

        public IMazeRoom GetRoomByPoint(MazePoint point)
        {
            _rooms.TryGetValue(point, out var result);
            return result;
        }
    }
}
