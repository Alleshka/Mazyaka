using Maze.Common;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    public class MoveResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public MoveFailureReason FailureReason { get; }
        public IMazeRoom NextRoom { get; }


        private MoveResult(bool success, IMazeRoom room, MoveFailureReason failureReason = MoveFailureReason.None, string message = null)
        {
            IsSuccess = success;
            FailureReason = failureReason;
            Message = message;
            NextRoom = room;
        }

        private MoveResult(IMazeRoom nextRoom) : this(true, nextRoom)
        {

        }

        private MoveResult (MoveFailureReason reason, string message) : this (false, null, reason, message)
        {

        }

        public static MoveResult Success(IMazeRoom room) => new MoveResult(room);
        public static MoveResult Failed(MoveFailureReason reason, string message = null) => new MoveResult(reason, message);
    }
}
