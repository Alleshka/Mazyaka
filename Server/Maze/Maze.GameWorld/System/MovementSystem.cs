using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.GameWorld.TraversalPolicies;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameWorld.System
{
    internal class MovementSystem : BaseSystem
    {
        private static readonly DefaultTraversalPolicy _defaultPolicy = new DefaultTraversalPolicy();

        public MovementSystem()
        {
        }

        public override void Run(GameContext gameContext)
        {
            var world = gameContext.State;
            var mazeRegistry = gameContext.Registry;

            foreach (var e in world.Query<MoveIntent>())
            {
                var position = world.Get<RoomPosition>(e);
                var intent = world.Get<MoveIntent>(e);

                var mazeInfo = GetMazeForPlayerOrDefault(e, world, mazeRegistry);
                var room = mazeInfo?.MazeStructure.GetRoomByID(position.RoomId);
                var connection = room?.GetConnection(intent.Direction);

                if (connection == null)
                {
                    world.Add(e, new MoveBlockedNoConnectionEvent());
                    continue;
                }

                var ctx = BuildContext(mazeInfo, connection, room, world);
                var policy = world.Has<TraversalPolicyComponent>(e)
                    ? world.Get<TraversalPolicyComponent>(e).Policy
                    : _defaultPolicy;

                var result = policy.CanTraverse(ctx, intent.Direction);

                if (result.IsExit)
                {
                    world.Add(e, new MoveExitEvent());
                    continue;
                }

                if (!result.CanPass)
                {
                    world.Add(e, new MoveBlockedByBlockerEvent(connection.Id, result.Blocker ?? "unknown"));
                    continue;
                }

                var nextRoom = connection.GetOther(room);
                position.RoomId = nextRoom.Id;
                world.Set(e, position);
                world.Add(e, new MoveSuccessEvent(position));
            }
        }

        private ConnectionContext BuildContext(IMazeInfo mazeInfo, IMazeConnection connection, IMazeRoom fromRoom, MazeState maze)
        {
            return new ConnectionContext(
                connection,
                fromRoom,
                mazeInfo.Metadata,
                maze.GetConnectionConditions(connection.Id)
            );
        }
    }
}
