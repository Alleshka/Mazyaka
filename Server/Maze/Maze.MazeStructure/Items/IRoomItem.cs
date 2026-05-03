using Maze.Common.Types;

namespace Maze.MazeStructure.Items
{
    public interface IRoomItem
    {
        EntityId ItemId { get; }
        EntityId RoomId { get; }
    }
}
