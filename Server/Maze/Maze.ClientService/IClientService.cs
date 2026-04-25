using Maze.Common;
using Maze.Common.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Maze.ClientService
{
    public interface IClientService : IAsyncDisposable
    {
        public Task ConnectAsync(HubConnection connection = null);
        public Task<CreateGameResponse> CreateGameAsync(int rows, int cols);
        public Task<PlayerJoinResponse> SetUserAsync(Guid gameId, int startRow, int startCol);
        public Task<MoveResponse> MoveAsync(Guid gameId, Guid userId, MoveDirection direction);
        public Task<DestroyWallResult> DestroyWallAsync(Guid gameId, Guid userId, MoveDirection direction);
    }
}
