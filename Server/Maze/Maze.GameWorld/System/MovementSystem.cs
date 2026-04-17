using Maze.GameWorld.Components;
using Maze.GameWorld.Evemts;
using Maze.MazeStructure;

namespace Maze.GameWorld.System
{
    internal class MovementSystem : ISystem
    {
        private IMazeInfo _mazeInfo;

        public MovementSystem(IMazeInfo mazeInfo)
        {
            _mazeInfo = mazeInfo;
        }

        public void Run(MazeState world)
        {

            foreach (var e in world.Query<MoveIntent>())
            {
                ref var position = ref world.Get<RoomPostition>(e);
                var intent = world.Get<MoveIntent>(e);

                var room = _mazeInfo.MazeStructure.GetRoomByID(position.RoomId);
                var connection = room.GetConnection(intent.Direction);

                if (connection == null)
                {
                    world.Add(e, new MoveBlockedNoConntectionEvent());
                    continue;
                }

                var meta = _mazeInfo.Metadata;


                if (meta.HasTypeTag(connection, MazeStructure.Metadata.ConnectionTypeTag.Boundary))
                {
                    world.Add(e, new MoveBlockedByBoundaryEvent(connection.Id));
                    continue;
                }

                if (meta.HasTypeTag(connection, MazeStructure.Metadata.ConnectionTypeTag.Exit))
                {
                    world.Add(e, new MoveExitEvent { });
                    continue;
                }

                var nextRoom = connection.GetOther(room);

                if (!meta.HasTypeTag(connection, MazeStructure.Metadata.ConnectionTypeTag.Passage))
                {
                    world.Add(e, new MoveBlockedByBlockerEvent(connection.Id, connection.GetType().Name));
                    continue;
                }

                position.RoomId = nextRoom.Id;
                world.Add(e, new MoveSuccessEvent { Postition = position });
            }
        }
    }
}
