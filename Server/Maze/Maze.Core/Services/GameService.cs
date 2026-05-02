using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using System;

namespace Maze.Core.Services
{
    public class GameService
    {
        private GameSessionManager _gameSessionManager;

        public GameService(GameSessionManager gameSessionManager)
        {
            _gameSessionManager = gameSessionManager;
        }

        public CreateGameResponse CreateGame(int rows, int cols)
        {
            var (gameId, _) = _gameSessionManager.CreateWithId(rows, cols);

            return new CreateGameResponse
            {
                GameId = gameId,
                Rows = rows,
                Cols = cols
            };
        }

        public PlayerJoinResponse SetUser(EntityId gameId, PlayerId player, int startRow, int startCol)
        {
            var session = _gameSessionManager.Get(gameId) ?? throw new Exception($"Game '{gameId}' not found.");

            EntityId startRoomId = session.Topology.GetRoomByCoordinates(startRow, startCol).Id;
            var userId = session.World.CreatePlayer(session.MazeInfo, startRoomId, player);
            var cell = startRoomId;

            return new PlayerJoinResponse
            {
                UserId = userId,
                CellId = cell
            };
        }

        public MoveResponse Move(EntityId gameId, PlayerId userId, MoveDirection direction)
        {
            var session = _gameSessionManager.Get(gameId) ?? throw new Exception($"Game '{gameId}' not found.");
            return session.World.ExecuteMove(userId, direction);
        }

        public DestroyWallResult DestroyWall(EntityId gameId, PlayerId userId, MoveDirection direction)
        {
            var session = _gameSessionManager.Get(gameId) ?? throw new Exception($"Game '{gameId}' not found.");
            var result = session.World.ExecuteDestroyWall(userId, direction);

            return result;
        }
    }
}
