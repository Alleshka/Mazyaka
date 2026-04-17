using Maze.GameWorld.Components;

namespace Maze.GameWorld.Evemts
{
    public struct MoveBlockedNoConntectionEvent { };
    public record struct MoveBlockedByBlockerEvent(int Id, string BlockerName);
    public record struct MoveBlockedByBoundaryEvent(int Id);
}
