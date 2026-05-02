using Maze.Common;
using Maze.GameWorld.Components;
using Maze.GameWorld.TraversalPolicies;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameWorld.Services
{
    public class ConnectionContextBuilder
    {
        internal ConnectionContext BuildContext(IMazeInfo mazeInfo, IMazeConnection connection, IMazeRoom fromRoom, MazeState maze)
        {
            return new ConnectionContext(
                connection,
                fromRoom,
                mazeInfo.Metadata,
                maze.GetConnectionConditions(connection.Id)
            );
        }

        internal ConnectionContext BuildContext(GameContext gameContext, Entity entity, MoveDirection dir)
        {   
            var mazeInfo = gameContext.Registry.Get(gameContext.State.Get<PlayerMaze>(entity).MazeId);
            var position = gameContext.State.Get<RoomPosition>(entity);
            var room = mazeInfo?.MazeStructure.GetRoomByID(position.RoomId);
            var connection = room?.GetConnection(dir);
            return BuildContext(mazeInfo, connection, room, gameContext.State);
        }
    }
}
