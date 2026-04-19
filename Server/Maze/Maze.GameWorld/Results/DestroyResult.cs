namespace Maze.GameWorld.Results
{
    public record DestroyResult(bool Success, int ConnectionId, string? FailReason)
    {
            public static DestroyResult SuccessResult(int connectionId) => new DestroyResult(true, connectionId, null);
            public static DestroyResult Failure(string reason) => new DestroyResult(false, -1, reason);
    }
}
