using Maze.Common.Types;
using Maze.GameWorld.Components;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;

namespace Maze.GameWorld.TraversalPolicies
{
    internal abstract record TraversalResult
    {
        public static Success Success(EntityId nextRoomId) => new Success(nextRoomId);

        public static Blocked Blocked(EntityId connectionId, string blocker) => new Blocked(connectionId, blocker);
        public static Blocked Blocked(IMazeConnection connection, IMazeMetadata metadata, ConnectionConditions conditions)
        {
            string blocker = ConnectionDisplayResolver.Resolve(connection, metadata, conditions);
            return Blocked(connection.Id, blocker);
        }
        public static Blocked Blocked(ConnectionContext ctx)
        {
            return Blocked(ctx.Connection, ctx.Metadata, ctx.Conditions);
        }

        public static ExitReached ExitReached() => new ExitReached();

        // reserved for the future ese
        // public static ExitBlocked ExitBlocked() => new ExitBlocked();
    }

    internal sealed record Success(EntityId NextRoomId) : TraversalResult;
    internal sealed record Blocked(EntityId ConnectionId, string Reason) : TraversalResult;
    internal sealed record ExitReached : TraversalResult;
    internal sealed record ExitBlocked : TraversalResult;
}
