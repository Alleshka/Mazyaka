using Maze.Common.Types;

namespace Maze.MazeStructure.Items
{
    public record KeyRoomItem(EntityId ItemId, EntityId RoomId, bool IsReal) : IRoomItem;
}
