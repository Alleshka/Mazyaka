using Maze.GameWorld.TraversalPolicies;

namespace Maze.GameWorld.Components
{
    public record struct TraversalPolicyComponent(ITraversalPolicy Policy);
}
