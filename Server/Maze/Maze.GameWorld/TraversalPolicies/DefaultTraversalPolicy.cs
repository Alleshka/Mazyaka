using Maze.GameWorld.Components;
using Maze.MazeStructure.Metadata;
using System;

namespace Maze.GameWorld.TraversalPolicies
{
    internal class DefaultTraversalPolicy : ITraversalPolicy
    {
        private static readonly Lazy<ITraversalPolicy> _lazy = new Lazy<ITraversalPolicy>(() => new DefaultTraversalPolicy(), true);
        public static ITraversalPolicy Instance => _lazy.Value;

        protected DefaultTraversalPolicy()
        {

        }

        public TraversalResult CanTraverse(ConnectionContext ctx)
        {
            if (ctx.IsBoundary && !ctx.IsExit) return TraversalResult.Blocked(ctx);
            if (ctx.Conditions.HasFlag(ConnectionConditions.Sealed)) return TraversalResult.Blocked(ctx);
            if (ctx.IsExit) return TraversalResult.ExitReached();

            // Runtime conditions checked before structural class — they override defaults.
            // Collapsed blocks movement even through a destroyed wall (rubble fills the gap).
            var nextRoom = ctx.Connection.GetOther(ctx.FromRoom);
            if (ctx.Conditions.HasFlag(ConnectionConditions.Destroyed)) return TraversalResult.Success(nextRoom.Id);

            var connectionClass = ctx.Metadata.GetClass(ctx.Connection);
            return connectionClass switch
            {
                ConnectionClass.Passage => TraversalResult.Success(nextRoom.Id),
                _ => TraversalResult.Blocked(ctx)
            };
        }

        // reserved for future use
        private static TraversalResult CanTraverseOneWay(ConnectionContext ctx)
        {
            // Convention: OneWayPassage is traversable from RoomA side only.
            bool comingFromAllowedSide = ctx.Connection.RoomA.Id == ctx.FromRoom.Id;
            return comingFromAllowedSide ? TraversalResult.Success(ctx.Connection.GetOther(ctx.FromRoom).Id) : TraversalResult.Blocked(ctx);
        }
    }
}
