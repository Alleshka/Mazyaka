using Maze.Common;

namespace Maze.GameWorld.TraversalPolicies
{
    internal interface ITraversalPolicy
    {
        TraversalResult CanTraverse(ConnectionContext ctx);
    }
}
