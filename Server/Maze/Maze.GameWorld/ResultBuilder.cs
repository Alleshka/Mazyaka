using Maze.Common.DTO;
using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.GameWorld.Results;
using Maze.MazeStructure.MazeSites;
using System;
using System.Linq;

namespace Maze.GameWorld
{
    internal class ResultBuilder
    {
        public ActionResult Build(MazeState w, EcsEntity e)
        {
            return new ActionResult()
            {
                MoveResult = BuildMoveResult(w, e),
                DestroyResult = BuildDestroyResult(w, e)
            };
        }

        private MoveResponse BuildMoveResult(MazeState w, EcsEntity e)
        {
            PickedUpItem[] pickedUp = null;
            if (w.Has<ItemsPickedUpEvent>(e))
            {
                var ev = w.Get<ItemsPickedUpEvent>(e);
                pickedUp = ev.Items.Select(x => new PickedUpItem(x.ItemId, x.GetType().Name)).ToArray();
            }

            if (w.Has<MoveBlockedByBlockerEvent>(e))
            {
                var blocker = w.Get<MoveBlockedByBlockerEvent>(e);
                return MoveResponse.Blocked(new MoveBlocker() { BlockerId = blocker.Id, BlockedConnectionType = blocker.BlockerName });
            }

            if (w.Has<MoveBlockedNoConnectionEvent>(e))
            {
                return MoveResponse.Blocked(new MoveBlocker() { BlockerId = EntityId.Empty, BlockedConnectionType = WorldEdgeSite.Instance.GetType().Name });
            }

            if (w.Has<MoveSuccessEvent>(e))
            {
                return new MoveResponse { IsSuccess = true, PickedUpItems = pickedUp };
            }

            if (w.Has<MoveExitEvent>(e))
            {
                return MoveResponse.Won();
            }

            return null;
        }

        private DestroyWallResult BuildDestroyResult(MazeState w, EcsEntity e)
        {
            int grenadesCount = w.Has<Grenades>(e) ? w.Get<Grenades>(e).Count : 0;

            if (w.Has<WallDestroyedEvent>(e))
            {
                var ev = w.Get<WallDestroyedEvent>(e);
                return DestroyWallResult.Success(ev.ConnectionId, grenadesCount);
            }

            if (w.Has<DestroyFailedEvent>(e))
            {
                var ev = w.Get<DestroyFailedEvent>(e);
                return DestroyWallResult.Failure(ev.Reason, grenadesCount);
            }

            return null;
        }
    }
}
