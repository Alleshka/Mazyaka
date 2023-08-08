using System.Collections.Generic;

namespace Maze.MazeStructure
{
    internal class SimpleMaze : IMaze
    {
        private class Point
        {
            public int Line { get; set; }
            public int Column { get; set; }

            public Point(int line, int col)
            {
                Line = line;
                Column = col;
            }

            public override bool Equals(object obj)
            {
                var p = obj as Point;
                if (p == null)
                {
                    return false;
                }

                return p.Line == Line && p.Column == Column;
            }

            public override string ToString()
            {
                return $"({Line}; {Column})";
            }

            public override int GetHashCode()
            {
                int hash = 17;
                hash = hash * 23 + Line.GetHashCode();
                hash = hash * 23 + Column.GetHashCode();
                return hash;
            }
        }

        private Dictionary<SimpleMaze.Point, IMazeRoom> _rooms;

        public int LineCount => 10;

        public int ColCount => 10;

        public SimpleMaze()
        {
            _rooms = new Dictionary<SimpleMaze.Point, IMazeRoom>();
        }

        public void AddRoom(IMazeRoom room)
        {
            var point = new SimpleMaze.Point(room.Line, room.Column);
            _rooms.Add(point, room);
        }

        public IMazeRoom GetRoomByCoordinates(int line, int col)
        {
            var point = new SimpleMaze.Point(line, col);
            _rooms.TryGetValue(point, out var result);
            return result;
        }
    }
}
