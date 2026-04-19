using Maze.GameWorld.Components;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;

namespace Maze.GameWorld.TraversalPolicies
{
    public record struct TraversalResult(bool CanPass, bool IsExit, string? Blocker = null)
    {
        public static readonly TraversalResult Exit = new TraversalResult(true, true);
        public static TraversalResult Success() => new TraversalResult(true, false);
        public static TraversalResult Blocked(string blocker) => new TraversalResult(false, false, blocker);
        public static TraversalResult Blocked(IMazeConnection connection, IMazeMetadata metadata, ConnectionConditions conditions)
        {
            string blocker = ConnectionDisplayResolver.Resolve(connection, metadata, conditions);
            return Blocked(blocker);
        }
        public static TraversalResult Blocked(ConnectionContext ctx)
        {
            return Blocked(ctx.Connection, ctx.Metadata, ctx.Conditions);
        }
    }
}
