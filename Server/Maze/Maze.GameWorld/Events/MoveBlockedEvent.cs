using Maze.Common.Types;

namespace Maze.GameWorld.Events
{
    public struct MoveBlockedNoConnectionEvent { }
    public record struct MoveBlockedByBlockerEvent(EntityId Id, string BlockerName);
}
