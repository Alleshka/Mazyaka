using Maze.Common.Types;

namespace Maze.Common.DTO
{
    public class MoveBlocker
    {
        public EntityId BlockerId { get; set; } = EntityId.Empty;
        public string BlockedConnectionType { get; set; }
    }
}
