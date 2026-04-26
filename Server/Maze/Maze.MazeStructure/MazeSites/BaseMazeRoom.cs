using Maze.Common;
using Maze.Common.Types;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure.MazeSites
{
    public class BaseMazeRoom : IMazeRoom
    {
        public EntityId Id { get; protected set; }

        public IReadOnlyDictionary<MoveDirection, IMazeConnection> Connections => _connections;

        private Dictionary<MoveDirection, IMazeConnection> _connections;

        public BaseMazeRoom(EntityId id)
        {
            Id = id;
            _connections = new Dictionary<MoveDirection, IMazeConnection>();
        }

        public IMazeConnection GetConnection(MoveDirection direction)
        {
            _connections.TryGetValue(direction, out var connection);
            return connection;
        }

        public void AddConnection(MoveDirection direction, IMazeConnection connection)
        {
            _connections[direction] = connection;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (_connections.TryGetValue(MoveDirection.Left, out var left))
            {
                builder.Append($"L:{left};");
            }

            if (_connections.TryGetValue(MoveDirection.Up, out var up))
            {
                builder.Append($"U:{up};");
            }

            if (_connections.TryGetValue(MoveDirection.Down, out var down))
            {
                builder.Append($"D:{down};");
            }

            if (_connections.TryGetValue(MoveDirection.Right, out var right))
            {
                builder.Append($"R:{right};");
            }

            return builder.ToString();
        }
    }
}
