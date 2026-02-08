using Maze.GameWorld.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.GameWorld.System
{
    internal class RoomEnterSystem
    {
        public void Process(MazeState world)
        {
            foreach (var e in world.Query<EnteredRoomEvent>())
            {
                var room = world.Get<EnteredRoomEvent>(e).RoomId;

                Console.WriteLine($"Entity {e} entered room {room}");

                world.Remove<EnteredRoomEvent>(e);
            }
        }
    }
}
