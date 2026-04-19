using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.MazeStructure;

namespace Maze.GameWorld.System
{
    internal class DestroyWallSystem : ISystem
    {
        public DestroyWallSystem()
        {

        }

        public void Run(MazeState world)
        {
            foreach (var e in world.Query<DestroyWallIntent>())
            {
                var position = world.Get<RoomPosition>(e);
                var intent = world.Get<DestroyWallIntent>(e);

                var mazeInfo = world.Get<PlayerMaze>(e).MazeInfo;
                var room = mazeInfo.MazeStructure.GetRoomByID(position.RoomId);
                var connection = room.GetConnection(intent.Direction);

                if (connection == null)
                {
                    world.Add(e, new DestroyFailedEvent("no connection in that direction"));
                    continue;
                }

                if (!world.Has<Grenades>(e))
                {
                    world.Add(e, new DestroyFailedEvent("no grenades left"));
                    continue;
                }

                var grenades = world.Get<Grenades>(e);
                if (grenades.Count <= 0)
                {
                    world.Add(e, new DestroyFailedEvent("no grenades left"));
                    continue;
                }

                world.Add(e, new Grenades(grenades.Count - 1));
                world.AddConnectionCondition(connection.Id, ConnectionConditions.Destroyed);
                world.Add(e, new WallDestroyedEvent(connection.Id));
            }
        }
    }
}
