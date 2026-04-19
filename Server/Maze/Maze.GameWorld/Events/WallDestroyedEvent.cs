namespace Maze.GameWorld.Events
{
    public record struct WallDestroyedEvent(int ConnectionId);
    public record struct DestroyFailedEvent(string Reason);
}
