using Maze.Common.Types;
using Maze.Core.ConnectionRegistry;
using System.Collections.Concurrent;

namespace Maze.Server
{
    public class InMemoryConnectionRegistry : IConnectionRegistry
    {
        private readonly ConcurrentDictionary<PlayerId, string> _playerToConnection = new();
        private readonly ConcurrentDictionary<string, PlayerId> _connectionToPlayer = new();

        public string? GetConnectionId(PlayerId playerId)
        {
            _playerToConnection.TryGetValue(playerId, out var connectionId);
            return connectionId;
        }

        public void Register(PlayerId playerId, string connectionId)
        {
            if (_playerToConnection.TryGetValue(playerId, out string conn))
            {
                Unregister(conn);
            }

            _playerToConnection[playerId] = connectionId;
            _connectionToPlayer[connectionId] = playerId;
        }

        public void Unregister(string connectionId)
        {
            if (_connectionToPlayer.TryRemove(connectionId, out var playerId))
            {
                _playerToConnection.TryRemove(playerId, out _);
            }
        }
    }
}
