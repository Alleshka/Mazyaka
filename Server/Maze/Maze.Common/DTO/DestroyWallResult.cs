namespace Maze.Common.DTO
{
    public class DestroyWallResult
    {
        public bool IsSuccess { get; set; }
        public int ConnectionId { get; set; }
        public string Message { get; set; }
        public int Grenades { get; set; }

        public DestroyWallResult(bool isSuccess, int connectionId, int grenades, string message = "")
        {
            IsSuccess = isSuccess;
            ConnectionId = connectionId;
            Message = message;
            Grenades = grenades;
        }

        public static DestroyWallResult Success(int connectionId, int grenadesCount)
        {
            return new DestroyWallResult(true, connectionId, grenadesCount);
        }

        public static DestroyWallResult Failure(string message, int grenadesCount)
        {
            return new DestroyWallResult(false, -1, grenadesCount, message);
        }
    }
}
