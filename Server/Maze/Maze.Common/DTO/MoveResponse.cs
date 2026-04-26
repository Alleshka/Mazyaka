using Maze.Common.Types;

namespace Maze.Common.DTO
{
    public class MoveResponse
    {
        public bool Success { get; set; }
        public bool Win { get; set; }
        public EntityId CellId { get; set; }

        public MoveDirection? BlockedDirection { get; set; }
        public MoveBlocker MoveBlocker { get; set; }
    }
}
