using Maze.Common.Types;
using System.Collections.Generic;

namespace Maze.Common.DTO
{
    public class MoveResponse
    {
        public bool IsSuccess { get; init; }
        public bool Win { get; init; }
        public MoveBlocker Blocker { get; init; }
        public bool RequiresKeySelection { get; init; }
        public IReadOnlyList<EntityId> AvailableKeys { get; init; }

        public MoveResponse()
        {

        }

        public static MoveResponse Success() => new MoveResponse() { IsSuccess = true };
        public static MoveResponse Won() => new MoveResponse() { IsSuccess = true, Win = true };
        public static MoveResponse Blocked(MoveBlocker blocker) => new MoveResponse() { IsSuccess = false, Blocker = blocker };
        public static MoveResponse NeedsKey(IReadOnlyList<EntityId> keys) => new MoveResponse() { IsSuccess = false, RequiresKeySelection = true, AvailableKeys = keys };
    }
}
