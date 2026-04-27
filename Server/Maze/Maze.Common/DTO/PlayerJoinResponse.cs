using Maze.Common.Types;

namespace Maze.Common.DTO
{
    public class PlayerJoinResponse
    {
        public PlayerId UserId { get; set; }
        public EntityId CellId { get; set; }
    }

}
