using Maze.Common;
using Maze.Common.Types;
using System.Collections.Generic;

namespace Maze.MazeStructure.MazeSites
{
    // Null-object for the "outside" side of boundary connections.
    // Replaces null so GetOther() never throws on boundary connections.
    public sealed class WorldEdgeSite : IMazeRoom
    {
        public static readonly WorldEdgeSite Instance = new WorldEdgeSite();
        private static readonly IReadOnlyDictionary<MoveDirection, IMazeConnection> _connections = new Dictionary<MoveDirection, IMazeConnection>();

        private WorldEdgeSite() { }

        public EntityId Id => EntityId.Empty;

        public IReadOnlyDictionary<MoveDirection, IMazeConnection> Connections => _connections;

        public void AddConnection(MoveDirection direction, IMazeConnection connection) { }
        public IMazeConnection GetConnection(MoveDirection direction) => null;
    }
}
