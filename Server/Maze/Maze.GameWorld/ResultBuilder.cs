using Maze.Common.DTO;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.GameWorld.Results;

namespace Maze.GameWorld
{
    internal class ResultBuilder
    {
        public ActionResult Build(MazeState w, Entity e)
        {
            return new ActionResult()
            {
                MoveResult = BuildMoveResult(w, e),
                DestroyResult = BuildDestroyResult(w, e)
            };
        }

        private MoveResult? BuildMoveResult(MazeState w, Entity e)
        {
            if (w.Has<MoveBlockedByBlockerEvent>(e))
            {
                var blocker = w.Get<MoveBlockedByBlockerEvent>(e);
                return MoveResult.Blocked(-1, new Blocker(blocker.Id, blocker.BlockerName));
            }
            if (w.Has<MoveBlockedNoConnectionEvent>(e))
            {
                return MoveResult.Blocked(-1, new Blocker(-1, "No connection"));
            }
            if (w.Has<MoveSuccessEvent>(e))
            {
                var pos = w.Get<RoomPosition>(e);
                return MoveResult.Success(pos.RoomId);
            }
            if (w.Has<MoveExitEvent>(e))
            {
                return MoveResult.Success(-1, win: true);
            }
            return null;
        }

        private DestroyWallResult? BuildDestroyResult(MazeState w, Entity e)
        {
            if (w.Has<WallDestroyedEvent>(e))
            {
                var ev = w.Get<WallDestroyedEvent>(e);
                return DestroyWallResult.Success(ev.ConnectionId);
            }

            if (w.Has<DestroyFailedEvent>(e))
            {
                var ev = w.Get<DestroyFailedEvent>(e);
                return DestroyWallResult.Failure(ev.Reason);
            }

            return null;
        }
    }
}
