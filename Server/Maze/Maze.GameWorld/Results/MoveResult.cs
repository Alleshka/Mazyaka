using Maze.Common.Types;

namespace Maze.GameWorld.Results
{
    public record Blocker(EntityId Id, string Name);

    public record MoveResult(EntityId RoomId, bool Win, bool SuccessMove, Blocker? BlockedBy)
    {
        public static MoveResult Success(EntityId roomId, bool win = false) => new MoveResult(roomId, win, true, null);
        public static MoveResult Blocked(Blocker blocker) => new MoveResult(EntityId.Empty, false, false, blocker);
    }
}
