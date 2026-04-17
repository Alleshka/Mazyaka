using Maze.GameWorld.Components;
using Maze.GameWorld.Evemts;
using System;

namespace Maze.GameWorld
{
    internal class ResultBuilder
    {
        public ActionResult Build(MazeState w, Entity e)
        {
            var result = new ActionResult();

            if (w.Has<MoveBlockedByBlockerEvent>(e))
            {
                var blocker = w.Get<MoveBlockedByBlockerEvent>(e);
                result.SuccessMove = false;
                result.BlockedBy = new Blocker()
                {
                    Id = blocker.Id,
                    Name = blocker.BlockerName
                };

                Console.WriteLine($"{e.Id} was blocked by {blocker.BlockerName} with id {blocker.Id}");
            }

            if (w.Has<MoveBlockedByBoundaryEvent>(e))
            {
                var blocker = w.Get<MoveBlockedByBoundaryEvent>(e);
                result.SuccessMove = false;
                result.BlockedBy = new Blocker()
                {
                    Id = blocker.Id,
                    Name = "Boundary"
                };

                Console.WriteLine($"{e.Id} was blocked by Boundary with id {blocker.Id}");
            }

            if (w.Has<MoveBlockedNoConntectionEvent>(e))
            {
                result.SuccessMove = false;
                Console.WriteLine($"{e.Id} was blocked by no connection");
            }
            
            if (w.Has<MoveSuccessEvent>(e))
            {
                result.SuccessMove = true;
                var pos = w.Get<RoomPostition>(e);
                result.RoomId = pos.RoomId;

                Console.WriteLine($"{e.Id} moved to room {pos.RoomId}");
            }

            if (w.Has<MoveExitEvent>(e))
            {
                result.Win = true;
                Console.WriteLine($"{e.Id} has won the game!");
            }

            return result;
        }
    }
}
