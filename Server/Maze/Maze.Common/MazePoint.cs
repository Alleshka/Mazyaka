namespace Maze.Common
{
    public class MazePoint
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public MazePoint()
        {

        }

        public MazePoint(int line, int col)
        {
            Row = line;
            Column = col;
        }

        public override bool Equals(object obj)
        {
            var p = obj as MazePoint;
            if (p == null)
            {
                return false;
            }

            return p.Row == Row && p.Column == Column;
        }

        public override string ToString()
        {
            return $"({Row}; {Column})";
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Row.GetHashCode();
            hash = hash * 23 + Column.GetHashCode();
            return hash;
        }
    }
}
