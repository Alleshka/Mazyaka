using Maze.Common;
using Maze.Common.DTO;
using Maze.Common.Types;
using System;

namespace Maze.Core.Services
{
    public class GameService
    {
        // TODO: Move to DI
        // Don't want create GameSession public for now
        private GameSessionManager _gameSessionManager = new GameSessionManager();

        public GameService()
        {
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
            var result = session.World.ExecuteMove(userId, direction);

            if (result.Win == true)
            {
                return new MoveResponse { Success = true, Win = true };
            }

            if (!result.SuccessMove)
            {
                return new MoveResponse
                {
                    Success = false,
                    BlockedDirection = direction,
                    MoveBlocker = new MoveBlocker()
                    {
                        BlockerId = result?.BlockedBy?.Id ?? EntityId.Empty,
                        BlockedConnectionType = result?.BlockedBy?.Name
                    }
                };
            }

            return new MoveResponse
            {
                Success = true,
                CellId = result.RoomId // CellRevealBuilder.Build(result.RoomId, session)
            };
        }

        public DestroyWallResult DestroyWall(EntityId gameId, PlayerId userId, MoveDirection direction)
        {
            var session = _gameSessionManager.Get(gameId) ?? throw new Exception($"Game '{gameId}' not found.");
            var result = session.World.ExecuteDestroyWall(userId, direction);

            return result;
        }
    }
}
