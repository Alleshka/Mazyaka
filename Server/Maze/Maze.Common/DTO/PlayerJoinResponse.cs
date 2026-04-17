using System;

namespace Maze.Common.DTO
{
    public class PlayerJoinResponse
    {
        public Guid UserId { get; set; }
        public int CellId { get; set; }
    }

}
