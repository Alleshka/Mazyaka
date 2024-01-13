using System;

namespace Maze.Common
{
    [Flags]
    public enum MoveDirection
    {
        None = 0,
        Up = 1 << 0,
        Right = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3
    }

    public enum MoveStatus
    {
        None = 0,
        Success = 1,
        Failure = 2,
        Winner = 3,
        Looser = 4
    }

    public class MoveResult
    {
        public MoveStatus Status { get; set; }
        public MazePoint Point { get; set; }
        public string MazeSite { get; set; }

        public MoveResult()
        {

        }
    }
}
