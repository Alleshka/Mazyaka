using Maze.Common;
using Maze.GameWorld.Components;
using Maze.MazeStructure.Metadata;

namespace Maze.GameWorld.TraversalPolicies
{
    public class DefaultTraversalPolicy : ITraversalPolicy
    {
        public TraversalResult CanTraverse(ConnectionContext ctx, MoveDirection dir)
        {
            if (ctx.IsBoundary && !ctx.IsExit) return TraversalResult.Blocked(ctx);
            if (ctx.IsExit) return TraversalResult.Exit;

            // Runtime conditions checked before structural class — they override defaults.
            // Collapsed blocks movement even through a destroyed wall (rubble fills the gap).
            if (ctx.Conditions.HasFlag(ConnectionConditions.Destroyed)) return TraversalResult.Success();

            var connectionClass = ctx.Metadata.GetClass(ctx.Connection);
            return connectionClass switch
            {
                ConnectionClass.Passage => TraversalResult.Success(),
                _ => TraversalResult.Blocked(ctx)
            };
        }

        private static TraversalResult CanTraverseOneWay(ConnectionContext ctx)
        {
            // Convention: OneWayPassage is traversable from RoomA side only.
            bool comingFromAllowedSide = ctx.Connection.RoomA.Id == ctx.FromRoom.Id;
            return comingFromAllowedSide ? TraversalResult.Success() : TraversalResult.Blocked(ctx);
        }
    }
}
