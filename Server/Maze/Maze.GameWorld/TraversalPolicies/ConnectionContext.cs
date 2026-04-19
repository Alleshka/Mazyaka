using Maze.GameWorld.Components;
using Maze.MazeStructure.Metadata;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameWorld.TraversalPolicies
{
    public record struct ConnectionContext(
        IMazeConnection Connection,
        IMazeRoom FromRoom,
        IMazeMetadata Metadata,
        ConnectionConditions Conditions
    )
    {
        public bool IsBoundary => Metadata.IsBoundary(Connection);
        public bool IsExit => Metadata.IsExit(Connection);
    }
}
