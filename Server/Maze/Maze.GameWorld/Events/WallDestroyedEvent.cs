using Maze.Common.Types;

namespace Maze.GameWorld.Events
{
    public record struct WallDestroyedEvent(EntityId ConnectionId);
    public record struct DestroyFailedEvent(string Reason);
}
