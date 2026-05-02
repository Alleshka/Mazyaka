using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.GameWorld.Services;
using Maze.GameWorld.TraversalPolicies;
using Maze.MazeStructure;

namespace Maze.GameWorld.System
{
    internal class MovementSystem : BaseSystem
    {
        private ConnectionContextBuilder _connectionContextBuilder;

        public MovementSystem(ConnectionContextBuilder connectionContextBuilder)
        {
            _connectionContextBuilder = connectionContextBuilder;
        }

        public override void Run(GameContext gameContext)
        {
            var world = gameContext.State;
            var mazeRegistry = gameContext.Registry;

            TraversalResult result;
            foreach (var e in world.Query<MoveIntent>())
            {
                var position = world.Get<RoomPosition>(e);

                if (world.Has<CachedTraversalResult>(e))
                {
                    result = world.Get<CachedTraversalResult>(e).Result;
                }
                else
                {
                    var intent = world.Get<MoveIntent>(e);
                    var mazeInfo = GetMazeForPlayerOrDefault(e, world, mazeRegistry);
                    var room = mazeInfo?.MazeStructure.GetRoomByID(position.RoomId);
                    var connection = room?.GetConnection(intent.Direction);

                    if (connection == null)
                    {
                        world.Add(e, new MoveBlockedNoConnectionEvent());
                        continue;
                    }

                    var ctx = _connectionContextBuilder.BuildContext(mazeInfo, connection, room, world);
                    var policy = world.Has<TraversalPolicyComponent>(e)
                        ? world.Get<TraversalPolicyComponent>(e).Policy
                        : DefaultTraversalPolicy.Instance;

                    result = policy.CanTraverse(ctx);
                }

                switch (result)
                {
                    case ExitReached:
                        {
                            world.Add(e, new MoveExitEvent());
                            break;
                        }
                    case Blocked blocked:
                        {
                            world.Add(e, new MoveBlockedByBlockerEvent(blocked.ConnectionId, blocked.Reason ?? "unknown"));
                            break;
                        }
                    case Success success:
                        {
                            position.RoomId = success.NextRoomId;
                            world.Set(e, position);
                            world.Add(e, new MoveSuccessEvent());
                            break;
                        }
                }
            }
        }
    }
}
