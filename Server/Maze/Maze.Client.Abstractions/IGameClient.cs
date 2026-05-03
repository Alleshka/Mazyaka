using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using System;
using System.Threading.Tasks;

namespace Maze.Client.Abstractions
{
    public interface IGameClient : IAsyncDisposable
    {
        public Task<CreateGameResponse> CreateGameAsync(int rows, int cols);
        public Task<PlayerJoinResponse> SetUserAsync(EntityId gameId, int startRow, int startCol);
        public Task<MoveResponse> MoveAsync(EntityId gameId, MoveDirection direction, EntityId? keyId = null);
        public Task<DestroyWallResult> DestroyWallAsync(EntityId gameId, MoveDirection direction);
    }
}
