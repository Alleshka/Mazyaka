using Maze.Common;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    internal class SimpleMaze : IMaze
    {
        private Dictionary<MazePoint, IMazeRoom> _rooms;

        public int LineCount => 10;

        public int ColCount => 10;

        public SimpleMaze()
        {
            _rooms = new Dictionary<MazePoint, IMazeRoom>();
        }

        public void AddRoom(IMazeRoom room)
        {
            var point = new MazePoint(room.Line, room.Column);
            _rooms.Add(point, room);
        }

        public IMazeRoom GetRoomByCoordinates(int line, int col)
        {
            var point = new MazePoint(line, col);
            _rooms.TryGetValue(point, out var result);
            return result;
        }

        public IMazeRoom GetRoomByPoint(MazePoint point)
        {
            _rooms.TryGetValue(point, out var result);
            return result;
        }
    }
}
