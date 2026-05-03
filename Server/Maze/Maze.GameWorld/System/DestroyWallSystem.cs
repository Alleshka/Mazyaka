using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;

namespace Maze.GameWorld.System
{
    internal class DestroyWallSystem : BaseSystem
    {
        public DestroyWallSystem()
        {

        }

        public override void Run(GameContext gameContext)
        {
            var world = gameContext.WorldState;
            var mazeRuntime = gameContext.MazeRuntime;
            var mazeRegistry = gameContext.Registry;

            foreach (var e in world.Query<DestroyWallIntent>())
            {
                var position = world.Get<RoomPosition>(e);
                var intent = world.Get<DestroyWallIntent>(e);

                var mazeId = world.Get<PlayerMaze>(e).MazeId;
                var mazeInfo = GetMazeForPlayerOrDefault(e, world, mazeRegistry);
                var room = mazeInfo?.MazeStructure.GetRoomByID(position.RoomId);
                var connection = room?.GetConnection(intent.Direction);

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

                if (mazeInfo.Metadata.IsBoundary(connection))
                {
                    world.Add(e, new DestroyFailedEvent("cannot destroy boundary wall"));
                    continue;
                }

                mazeRuntime.AddConnectionCondition(mazeId, connection.Id, ConnectionConditions.Destroyed);
                world.Add(e, new WallDestroyedEvent(connection.Id));
            }
        }
    }
}
