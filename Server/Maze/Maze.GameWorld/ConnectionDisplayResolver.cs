using Maze.Common;
using Maze.GameWorld.Components;
using Maze.MazeStructure.MazeSites;
using Maze.MazeStructure.Metadata;

namespace Maze.GameWorld
{
    internal static class ConnectionDisplayResolver
    {
        public static string Resolve(
        IMazeConnection connection,
        IMazeMetadata metadata,
        ConnectionConditions conditions)
        {
            if (metadata.IsExit(connection)) return Constants.ConnectionNames.Exit;
            if (metadata.IsBoundary(connection)) return Constants.ConnectionNames.Boundary;
            if (conditions.HasFlag(ConnectionConditions.Destroyed)) return Constants.ConnectionNames.DestroyedWall;
            return Constants.ConnectionNames.Wall;
        }
    }
}
