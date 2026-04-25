using Maze.Common;
using Maze.Common.DTO;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maze.ClientService
{
    public class SignalRClientService : IClientService
    {
        protected readonly string _hubUrl;
        private HubConnection _connection;

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = null,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            IncludeFields = true,
        };

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
            return JsonSerializer.Deserialize<CreateGameResponse>(json);
        }

        public async Task<PlayerJoinResponse> SetUserAsync(Guid gameId, int startRow, int startCol)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.SetUser, gameId, startRow, startCol);
            return JsonSerializer.Deserialize<PlayerJoinResponse>(json);
        }

        public async Task<MoveResponse> MoveAsync(Guid gameId, Guid userId, MoveDirection direction)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.Move, gameId, userId, direction);
            return JsonSerializer.Deserialize<MoveResponse>(json);
        }

        public async Task<DestroyWallResult> DestroyWallAsync(Guid gameId, Guid userId, MoveDirection direction)
        {
            var json = await _connection.InvokeAsync<string>(Constants.HubMethods.DestroyWall, gameId, userId, direction);
            return JsonSerializer.Deserialize<DestroyWallResult>(json, JsonOptions);
        }
    }
}
