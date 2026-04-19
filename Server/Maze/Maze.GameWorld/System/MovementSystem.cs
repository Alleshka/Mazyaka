using Maze.Common;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using Maze.GameWorld.TraversalPolicies;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameWorld.System
{
    internal class MovementSystem : ISystem
    {
        private readonly IMazeInfo _mazeInfo;
        private static readonly DefaultTraversalPolicy _defaultPolicy = new DefaultTraversalPolicy();

        public MovementSystem(IMazeInfo mazeInfo)
        {
            _mazeInfo = mazeInfo;
        }

        public void Run(MazeState world)
        {
            foreach (var e in world.Query<MoveIntent>())
            {
                ref var position = ref world.Get<RoomPosition>(e);
                var intent = world.Get<MoveIntent>(e);

                var room = _mazeInfo.MazeStructure.GetRoomByID(position.RoomId);
                var connection = room.GetConnection(intent.Direction);

                if (connection == null)
                {
                    world.Add(e, new MoveBlockedNoConnectionEvent());
                    continue;
                }

                var ctx = BuildContext(connection, room, world);
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
                world.Add(e, new MoveSuccessEvent(position));
            }
        }

        private ConnectionContext BuildContext(IMazeConnection connection, IMazeRoom fromRoom, MazeState maze)
        {
            return new ConnectionContext(
                connection,
                fromRoom,
                _mazeInfo.Metadata,
                maze.GetConnectionConditions(connection.Id)
            );
        }
    }
}
