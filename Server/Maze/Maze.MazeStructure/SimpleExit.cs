using Maze.Common;
using Maze.Core;
using Maze.MazeStructure.Interfaces;

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

        public override MoveResult Enter(IMazePlayer player, MoveDirection direction)
        {
            return new MoveResult()
            {
                MazeSite = this.GetType().Name,
                Status = MoveStatus.Winner,
                Point = new MazePoint(Line, Col)
            };
        }
    }
}
