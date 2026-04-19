namespace Maze.GameWorld.Events
{
    public struct MoveBlockedNoConnectionEvent { }
    public record struct MoveBlockedByBlockerEvent(int Id, string BlockerName);
}
