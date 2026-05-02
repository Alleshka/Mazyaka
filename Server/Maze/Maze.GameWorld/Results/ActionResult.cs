using Maze.Common.DTO;

namespace Maze.GameWorld.Results
{
    public class ActionResult
    {
        public MoveResponse MoveResult { get; set; }
        public DestroyWallResult DestroyResult { get; set; }

        public static ActionResult Move(MoveResponse moveResult) => new ActionResult { MoveResult = moveResult };
        public static ActionResult Destroy(DestroyWallResult destroyResult) => new ActionResult { DestroyResult = destroyResult };
    }
}
