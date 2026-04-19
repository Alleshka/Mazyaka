namespace Maze.GameWorld.Results
{
    public class ActionResult
    {
        public MoveResult? MoveResult { get; set; }
        public DestroyResult? DestroyResult { get; set; }

        public static ActionResult Move(MoveResult moveResult) => new ActionResult { MoveResult = moveResult };
        public static ActionResult Destroy(DestroyResult destroyResult) => new ActionResult { DestroyResult = destroyResult };
    }
}
