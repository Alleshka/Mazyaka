using Maze.GameWorld.Components;

namespace Maze.GameWorld.Events
{
    public record struct MoveSuccessEvent(RoomPosition Position);
    public struct MoveExitEvent { }
}
