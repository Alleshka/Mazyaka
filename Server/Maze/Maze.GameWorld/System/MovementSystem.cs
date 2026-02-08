using Maze.Common;
using Maze.GameWorld.Components;
using System;

namespace Maze.GameWorld.System
{
    internal class MovementSystem
    {
        private readonly GameWorld _world;

        public MovementSystem(GameWorld world)
        {
            _world = world;
        }

        public bool TryMove(Entity e, MoveDirection direction)
        {
            ref var position = ref _world.State.Get<RoomPostition>(e);
            var room = _world.MazeInfo.MazeStructure.GetRoomByID(position.RoomId);
            var connection = room.GetConnection(direction);

            if (connection == null)
            {
                return false;
            }

            var meta = _world.MazeInfo.Metadata;

            if (meta.HasTypeTag(connection, MazeStructure.Metadata.ConnectionTypeTag.Boundary))
            {
                Console.WriteLine("Boundary in direction " + direction);
                return false;
            }

            if (meta.HasTypeTag(connection, MazeStructure.Metadata.ConnectionTypeTag.Exit))
            {
                _world.State.Add(e, new EnteredExitEvent { });
                return false;
            }

            if (!meta.HasTypeTag(connection, MazeStructure.Metadata.ConnectionTypeTag.Passage))
            {
                Console.WriteLine("No passage in direction " + direction);
                return false;
            }

            var nextRoom = connection.GetOther(room);
            position.RoomId = nextRoom.Id;
            _world.State.Add(e, new EnteredRoomEvent { RoomId = nextRoom.Id });
            return true;
        }
    }
}
