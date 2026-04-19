namespace Maze.GameWorld.Results
{
    public record Blocker(int Id, string Name);

    public record MoveResult(int RoomId, bool Win, bool SuccessMove, Blocker? BlockedBy)
    {
        public static MoveResult Success(int roomId, bool win = false) => new MoveResult(roomId, win, true, null);
        public static MoveResult Blocked(int roomId, Blocker blocker) => new MoveResult(roomId, false, false, blocker);
    }
}
