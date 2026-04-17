namespace Maze.Common.DTO
{
    public class MoveBlocker
    {
        public int BlockerId { get; set; } = -1;
        public string? BlockedConnectionType { get; set; }
    }
}
