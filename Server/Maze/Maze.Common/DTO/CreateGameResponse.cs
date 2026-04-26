
using Maze.Common.Types;

namespace Maze.Common.DTO
{
    public class CreateGameResponse
    {
        public EntityId GameId { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
    }
}
