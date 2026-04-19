using Maze.MazeStructure.MazeSites;
using System.Collections.Generic;

namespace Maze.MazeStructure.Metadata
{
    public class MazeMetadata : IMazeMetadata
    {
        private readonly Dictionary<IMazeConnection, ConnectionClass> _classes = new Dictionary<IMazeConnection, ConnectionClass>();
        private readonly HashSet<IMazeConnection> _exits = new HashSet<IMazeConnection>();

        public void SetClass(IMazeConnection connection, ConnectionClass cls)
        {
            _classes[connection] = cls;
        }

        public ConnectionClass GetClass(IMazeConnection connection)
        {
            return _classes.TryGetValue(connection, out var cls) ? cls : ConnectionClass.Wall;
        }

        public void MarkExit(IMazeConnection connection)
        {
            _exits.Add(connection);
        }

        public bool IsExit(IMazeConnection connection)
        {
            return _exits.Contains(connection);
        }

        public bool IsBoundary(IMazeConnection connection)
        {
            return connection.RoomA is WorldEdgeSite || connection.RoomB is WorldEdgeSite;
        }
    }
}
