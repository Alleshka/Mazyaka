namespace Maze.Common.DTO
{
    public class DestroyWallResult
    {
        public bool IsSuccess { get; set; }
        public int ConnectionId { get; set; }
        public string Message { get; set; }

        public DestroyWallResult(bool isSuccess, int connectionId, string message = "")
        {
            IsSuccess = isSuccess;
            ConnectionId = connectionId;
            Message = message;
        }

        public static DestroyWallResult Success(int connectionId)
        {
            return new DestroyWallResult(true, connectionId);
        }

        public static DestroyWallResult Failure(string message)
        {
            return new DestroyWallResult(false, -1, message);
        }
    }
}
