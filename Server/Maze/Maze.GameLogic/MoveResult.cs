using Maze.GameLogic.GameMazeSite;

namespace Maze.MazeStructure
{
    public class MoveResult
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public string? FailureReason { get; }
        public GameMazeRoom? NextRoom { get; }


        private MoveResult(bool success, GameMazeRoom? room, string? failureReason = null, string? message = null)
        {
            IsSuccess = success;
            FailureReason = failureReason;
            Message = message;
            NextRoom = room;
        }

        private MoveResult(GameMazeRoom nextRoom) : this(true, nextRoom)
        {

        }

        private MoveResult (string reason, string? message) : this (false, null, reason, message)
        {

        }

        public static MoveResult Success(GameMazeRoom room) => new MoveResult(room);
        public static MoveResult Failed(string reason, string? message = null) => new MoveResult(reason, message);
    }
}
