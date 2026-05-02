using Maze.GameWorld.TraversalPolicies;

namespace Maze.GameWorld.Components
{
    internal record struct TraversalPolicyComponent(ITraversalPolicy Policy);
}
