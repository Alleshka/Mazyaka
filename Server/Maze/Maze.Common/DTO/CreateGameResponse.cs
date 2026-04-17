
using System;

namespace Maze.Common.DTO
{
    public class CreateGameResponse
    {
        public Guid GameId { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
    }
}
