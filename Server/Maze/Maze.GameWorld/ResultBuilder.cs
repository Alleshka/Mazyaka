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


            if (w.Has<MoveBlockedByBoundaryEvent>(e) || w.Has<MoveBlockedByWallEvent>(e) || w.Has<MoveBlockedNoConntectionEvent>(e))
            {
                result.SuccessMove = false;
                Console.WriteLine($"{e.Id} was blocked by boundary or wall or no connection");
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
