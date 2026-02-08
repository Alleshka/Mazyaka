namespace Maze.Common
{
    public enum MoveFailureReason
    {
        None = 0,

        WallBlocked = 101,
        NonDestroyableWallBlocked = 102,

        MissingKey = 201
    }
}
