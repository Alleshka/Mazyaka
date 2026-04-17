using Maze.GameWorld.Components;

namespace Maze.GameWorld.Evemts
{
    public struct MoveBlockedNoConntectionEvent { };
    public record struct MoveBlockedByWallEvent(RoomPostition nextRoom);
    public struct MoveBlockedByBoundaryEvent { };
}
