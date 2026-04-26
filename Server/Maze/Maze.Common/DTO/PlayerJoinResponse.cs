using Maze.Common.Types;

namespace Maze.Common.DTO
{
    public class PlayerJoinResponse
    {
        public EntityId UserId { get; set; }
        public EntityId CellId { get; set; }
    }

}
