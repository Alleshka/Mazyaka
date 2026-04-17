using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;
using Maze.Common.DTO;
using System;
using System.Text.Json;
using Maze.Common;

namespace MazeGame.Network
{
    public class MazeSignalRClient : MonoBehaviour
    {
        public string HubUrl { get; set; } = "http://localhost:50774/mazehub";

        private HubConnection _connection;

        public async Task ConnectAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(HubUrl, options =>
                {
                    options.Transports = HttpTransportType.WebSockets;
                })
                .WithAutomaticReconnect()
                .Build();



            await _connection.StartAsync();
            Debug.Log($"[MazeSignalR] Connected to {HubUrl}");
        }

        private async void OnDestroy()
        {
            if (_connection != null)
                await _connection.DisposeAsync();
        }

        // Hub methods return string; we deserialize with JsonUtility (no external deps)

        public async Task<CreateGameResponse> CreateGameAsync(int rows, int cols)
        {
            var json = await _connection.InvokeAsync<string>("CreateGame", rows, cols);
            Debug.Log($"[MazeSignalR] CreateGame response: {json}");
            return JsonSerializer.Deserialize<CreateGameResponse>(json);
            // return JsonUtility.FromJson<CreateGameResponse>(json);
        }

        public async Task<PlayerJoinResponse> SetUserAsync(Guid gameId, int startRow, int startCol)
        {
            var json = await _connection.InvokeAsync<string>("SetUser", gameId, startRow, startCol);
            Debug.Log($"[MazeSignalR] SetUser response: {json}");
            return JsonSerializer.Deserialize<PlayerJoinResponse>(json);
        }

        public async Task<MoveResponse> MoveAsync(Guid gameId, Guid userId, MoveDirection direction)
        {
            var json = await _connection.InvokeAsync<string>("Move", gameId, userId, direction);
            return JsonSerializer.Deserialize<MoveResponse>(json);
        }
    }
}
