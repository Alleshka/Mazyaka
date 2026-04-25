using Maze.Common.DTO;

namespace Maze.GameWorld.Results
{
    public class ActionResult
    {
        public MoveResult? MoveResult { get; set; }
        public DestroyWallResult? DestroyResult { get; set; }

        public static ActionResult Move(MoveResult moveResult) => new ActionResult { MoveResult = moveResult };
        public static ActionResult Destroy(DestroyWallResult destroyResult) => new ActionResult { DestroyResult = destroyResult };
    }
}
