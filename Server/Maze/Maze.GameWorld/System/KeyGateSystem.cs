using Maze.Common;
using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.MazeStructure.Items;
using System.Linq;

namespace Maze.GameWorld.System
{
    internal class KeyGateSystem : BaseSystem
    {
        public override void Run(GameContext gameContext)
        {
            var world = gameContext.WorldState;
            var mazeRuntime = gameContext.MazeRuntime;
            var registry = gameContext.Registry;

            foreach (var (player, _, intent) in world.Query<MoveExitEvent, MoveIntent>())
            {
                if (intent.KeyId == null)
                    continue;

                var inventory = world.Get<Inventory>(player);
                if (!inventory.Has<KeyRoomItem>()) continue;

                var keys = inventory.Get<KeyRoomItem>();
                var keyItem = keys.FirstOrDefault(x => x.ItemId == intent.KeyId);
                if (keyItem == null)
                {
                    world.Remove<MoveExitEvent>(player);
                    world.Add(player, new ExceptionEvent("InvalidKey"));
                    continue;
                }

                inventory.Items.Remove(keyItem);
                if (keyItem.IsReal)
                {
                    continue; // Keep MoveExitEvent — ResultBuilder produces Win
                }

                var connectionId = GetConnectionId(world, registry, player, intent.Direction);
                if (connectionId.HasValue)
                {
                    var mazeId = world.Get<PlayerMaze>(player).MazeId;
                    mazeRuntime.AddConnectionCondition(mazeId, connectionId.Value, ConnectionConditions.Sealed);
                }

                world.Remove<MoveExitEvent>(player);
                world.Add(player, new MoveBlockedByBlockerEvent(
                    connectionId ?? EntityId.Empty,
                    nameof(ConnectionConditions.Sealed)));
            }
        }

        private static EntityId? GetConnectionId(MazeState world, MazeRegistry registry, EcsEntity player, MoveDirection direction)
        {
            if (!world.Has<PlayerMaze>(player)) return null;
            var mazeInfo = registry.Get(world.Get<PlayerMaze>(player).MazeId);
            var position = world.Get<RoomPosition>(player);
            var room = mazeInfo?.MazeStructure.GetRoomByID(position.RoomId);
            return room?.GetConnection(direction)?.Id;
        }
    }
}
