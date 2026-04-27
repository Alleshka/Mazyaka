using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maze.ClientService
{
    public class SignalRClientService : IClientService
    {
        protected readonly string _hubUrl;
        private HubConnection _connection;

        public SignalRClientService(string hubUrl)
        {
            _hubUrl = hubUrl;
        }

        public async Task ConnectAsync(HubConnection connection = null)
        {
            if (connection != null)
            {
                _connection = connection;
            }
            else
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl, options =>
                    {
                        options.Transports = HttpTransportType.WebSockets;
                    })
                    .AddJsonProtocol(options =>
                    {
                        JsonOptions.Configure(options.PayloadSerializerOptions);
                    })
                    .WithAutomaticReconnect()
                    .Build();
            }

            await _connection.StartAsync();
        }

        public ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                return _connection.DisposeAsync();
            }

            return default;
        }

        public async Task<CreateGameResponse> CreateGameAsync(int rows, int cols)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.CreateGame, rows, cols);
            return JsonSerializer.Deserialize<CreateGameResponse>(json, JsonOptions.Default);
        }

        public async Task<PlayerJoinResponse> SetUserAsync(EntityId gameId, int startRow, int startCol)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.SetUser, gameId, startRow, startCol);
            return JsonSerializer.Deserialize<PlayerJoinResponse>(json, JsonOptions.Default);
        }

        public async Task<MoveResponse> MoveAsync(EntityId gameId, EntityId userId, MoveDirection direction)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.Move, gameId.Value, userId.Value, direction);
            return JsonSerializer.Deserialize<MoveResponse>(json, JsonOptions.Default);
        }

        public async Task<DestroyWallResult> DestroyWallAsync(EntityId gameId, EntityId userId, MoveDirection direction)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.DestroyWall, gameId.Value, userId.Value, direction);
            return JsonSerializer.Deserialize<DestroyWallResult>(json, JsonOptions.Default);
        }
    }
}
