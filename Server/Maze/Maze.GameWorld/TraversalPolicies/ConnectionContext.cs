using Maze.Common;
using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;

namespace Maze.GameWorld.TraversalPolicies
{
    internal record struct ConnectionContext(
        IMazeConnection Connection,
        IMazeRoom FromRoom,
        IMazeMetadata Metadata,
        ConnectionConditions Conditions
    )
    {
        public bool IsBoundary => Metadata.IsBoundary(Connection);
        public bool IsExit => Metadata.IsExit(Connection);

        public static ConnectionContext Build (IMazeConnection connection, IMazeRoom fromRoom, EntityId mazeId, GameContext gameContext)
        {
            IMazeInfo mazeInfo = gameContext.Registry.Get(mazeId);
            ConnectionConditions conditions = gameContext.MazeRuntime.GetConnectionConditions(mazeId, connection.Id);
            return new ConnectionContext(connection, fromRoom, mazeInfo.Metadata, conditions);
        }

        public static ConnectionContext Build(GameContext gameContext, EcsEntity entity, MoveDirection dir)
        {
            var mazeID = gameContext.WorldState.Get<PlayerMaze>(entity).MazeId;
            var mazeInfo = gameContext.Registry.Get(gameContext.WorldState.Get<PlayerMaze>(entity).MazeId);
            var position = gameContext.WorldState.Get<RoomPosition>(entity);
            var room = mazeInfo?.MazeStructure.GetRoomByID(position.RoomId);
            var connection = room?.GetConnection(dir);
            ConnectionConditions conditions = gameContext.MazeRuntime.GetConnectionConditions(mazeID, connection.Id);
            return new ConnectionContext(connection, room, mazeInfo.Metadata, conditions);
        }
    }
}
