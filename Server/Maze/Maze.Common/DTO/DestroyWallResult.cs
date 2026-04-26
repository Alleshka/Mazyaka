using Maze.Common.Types;

namespace Maze.Common.DTO
{
    public class DestroyWallResult
    {
        public bool IsSuccess { get; set; }
        public EntityId ConnectionId { get; set; }
        public string Message { get; set; }
        public int Grenades { get; set; }

        public DestroyWallResult(bool isSuccess, EntityId connectionId, int grenades, string message = "")
        {
            IsSuccess = isSuccess;
            ConnectionId = connectionId;
            Message = message;
            Grenades = grenades;
        }

        public static DestroyWallResult Success(EntityId connectionId, int grenadesCount)
        {
            return new DestroyWallResult(true, connectionId, grenadesCount);
        }

        public static DestroyWallResult Failure(string message, int grenadesCount)
        {
            return new DestroyWallResult(false, EntityId.Empty, grenadesCount, message);
        }
    }
}
