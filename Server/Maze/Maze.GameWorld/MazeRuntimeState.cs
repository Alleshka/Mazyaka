using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.MazeStructure.Items;
using System.Collections.Generic;

namespace Maze.GameWorld
{
    internal record struct MazeRuntimeKey(EntityId MazeId, EntityId itemId);

    internal class MazeRuntimeState
    {
        public Dictionary<MazeRuntimeKey, ConnectionConditions> Conditions { get; } = new Dictionary<MazeRuntimeKey, ConnectionConditions>();
        public Dictionary<MazeRuntimeKey, List<IRoomItem>> RoomItems { get; } = new Dictionary<MazeRuntimeKey, List<IRoomItem>>();

        public void AddConnectionCondition(EntityId mazeId, EntityId connectionId, ConnectionConditions condition)
        {
            MazeRuntimeKey key = new MazeRuntimeKey(mazeId, connectionId);
            Conditions.TryGetValue(key, out var existing);
            Conditions[key] = existing | condition;
        }

        public ConnectionConditions GetConnectionConditions(EntityId mazeId, EntityId connectionId)
        {
            MazeRuntimeKey key = new MazeRuntimeKey(mazeId, connectionId);
            return Conditions.TryGetValue(key, out var c) ? c : ConnectionConditions.None;
        }

        public void AddRoomItem(EntityId mazeId, IRoomItem roomItem)
        {
            MazeRuntimeKey key = new MazeRuntimeKey(mazeId, roomItem.RoomId);

            if (!RoomItems.TryGetValue(key, out var items))
            {
                items = new List<IRoomItem>();
                RoomItems[key] = items;
            }
            
            items.Add(roomItem);
        }

        public List<IRoomItem> GetItems(EntityId mazeId, EntityId roomId)
        {
            MazeRuntimeKey key = new MazeRuntimeKey(mazeId, roomId);
            RoomItems.TryGetValue(key, out var items);

            return items;
        }
    }
}
