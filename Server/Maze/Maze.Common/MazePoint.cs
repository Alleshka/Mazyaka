namespace Maze.Common
{
    public class MazePoint
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public MazePoint(int line, int col)
        {
            Line = line;
            Column = col;
        }

        public override bool Equals(object obj)
        {
            var p = obj as MazePoint;
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
}
