using Maze.Common.Types;

namespace Maze.Core.ConnectionRegistry
{
    public interface IConnectionRegistry
    {
        void Register(PlayerId playerId, string connectionId);
        void Unregister(string connectionId);
        string? GetConnectionId(PlayerId playerId);
    }
}
