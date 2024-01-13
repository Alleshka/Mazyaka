using Maze.Common;
using Maze.GameLogic;
using Maze.MazeStructure;
using Maze.MazeStructure.Builder;
using Microsoft.AspNetCore.SignalR;

namespace Maze.Server.SignalR
{
    public class GameHub : Hub
    {
        private IGameStorage _gameStorage;
        public GameHub(IGameStorage gameStorage)
        {
            _gameStorage = gameStorage;
        }

        public async Task StartGame()
        {
            var game = new SimpleMazeGame();
            game.CreateMaze(new RecursiveMazeGenerator(), new SimpleMazeBuilder());

            var id = _gameStorage.AddGame(game);

            await Clients.All.SendAsync("StartGame", id);
        }

        public async Task SetPlayer(Guid gameId, int line, int col)
        {
            var game = _gameStorage.GetGame(gameId);
            game.SetPlayer(line, col);

            Console.WriteLine($"Set user to ({line}, {col})");

            await Clients.All.SendAsync("SetPlayer", true);
        }

        public async Task Move(Guid gameId, Guid userId, MoveDirection direction)
        {
            var game = _gameStorage.GetGame(gameId);
            var result = game.MovePlayer(userId, direction);

            Console.WriteLine($"Try to move user {userId} to {direction}");
            Console.WriteLine($"{result.Status}: {result.Point} - {result.MazeSite}");
            await Clients.All.SendAsync("MoveResult", result);
        }

        public async Task DestroyWall(Guid gameId, MoveDirection direction)
        {

        }
    }
}
