using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using System.Linq;

namespace Maze.GameWorld.System
{
    internal class PickupSystem : BaseSystem
    {
        public override void Run(GameContext gameContext)
        {
            var world = gameContext.WorldState;

            foreach (var e in world.Query<MoveSuccessEvent>())
            {
                var position = world.Get<RoomPosition>(e);
                var mazeId = world.Get<PlayerMaze>(e);
                var key = new MazeRuntimeKey(mazeId.MazeId, position.RoomId);

                if (!gameContext.MazeRuntime.RoomItems.TryGetValue(key, out var items) || !items.Any()) continue;

                var inventory = world.Get<Inventory>(e);
                var pickedUp = items.ToList();
                foreach (var item in pickedUp)
                {
                    inventory.Items.Add(item);
                    items.Remove(item);
                }

                world.Add(e, new ItemsPickedUpEvent(pickedUp));
            }
        }
    }
}
