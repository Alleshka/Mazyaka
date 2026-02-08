using Maze.Core.Common;
using Maze.MazeStructure.Interfaces;
using Maze.MazeStructure.MazeSites;

namespace Maze.MazeStructure
{
    public static class Extensions
    {
        public static bool CanPass(this IMazeRoom room, MoveDirection direction)
        {
            return room.GetMazeConnection(direction)?.CannPass(room) ?? false;
        }
    }
}
