using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Maze.ClientService
{
    public interface IClientService : IAsyncDisposable
    {
        public Task ConnectAsync(HubConnection connection = null);
        public Task<CreateGameResponse> CreateGameAsync(int rows, int cols);
        public Task<PlayerJoinResponse> SetUserAsync(EntityId gameId, int startRow, int startCol);
        public Task<MoveResponse> MoveAsync(EntityId gameId, EntityId userId, MoveDirection direction);
        public Task<DestroyWallResult> DestroyWallAsync(EntityId gameId, EntityId userId, MoveDirection direction);
    }
}
