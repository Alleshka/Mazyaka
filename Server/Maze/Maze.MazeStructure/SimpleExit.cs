using Maze.Common;

namespace Maze.MazeStructure
{
    internal class SimpleExit : BaseMazeSite, IMazeExit
    {
        public int Line { get; }

        public int Col { get; }

        public SimpleExit(int line, int col)
        {
            Line = line;
            Col = col;
        }

        public override MoveResult Enter(IMazePlayer player)
        {
            var result = base.Enter(player);
            result.Status = MoveStatus.Winner;
            result.Line = Line;
            result.Column = Col;
            return result;
        }
    }
}
