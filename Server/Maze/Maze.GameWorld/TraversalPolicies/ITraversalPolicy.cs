using Maze.Common;

namespace Maze.GameWorld.TraversalPolicies
{
    public interface ITraversalPolicy
    {
        TraversalResult CanTraverse(ConnectionContext ctx, MoveDirection dir);
    }
}
