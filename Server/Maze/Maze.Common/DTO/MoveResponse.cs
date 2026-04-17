using System;

namespace Maze.Common.DTO
{
    public class MoveResponse
    {
        public bool Success { get; set; }
        public bool Win { get; set; }
        public int CellId { get; set; }

        public MoveDirection? BlockedDirection { get; set; }
        public MoveBlocker? MoveBlocker { get; set; }
    }
}
