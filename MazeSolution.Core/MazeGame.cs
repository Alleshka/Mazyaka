using MazeSolution.Core.MazeStructrure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolution.Core
{
    public enum GameStatus
    {
        Lobby, 
        SendingMaze,
        Gaming,
        Finished
    }

    public class PlayerInfo<T> where T : ICell, new()
    {
        public bool IsReady { get; set; }
        public Maze<T> Maze { get; set; }
    }

    public abstract class BaseMazeGame<T> : BaseMazeObject where T : ICell, new()
    {
        protected Action<BaseMazeObject> _endGameAction;

        protected int MaxPlayers { get; set; }

        public GameStatus Status { get; protected set; }

        protected readonly Dictionary<Guid, PlayerInfo<T>> _players;

        private object _locker = new object();

        public BaseMazeGame(Action<BaseMazeObject> endGameAction)
        {
            _players = new Dictionary<Guid, PlayerInfo<T>>();
            _endGameAction = endGameAction;
            Status = GameStatus.Lobby;
        }

        public bool JoinGame(Guid userID)
        {
            lock (_locker)
            {
                if (_players.Count < MaxPlayers)
                {
                    _players.Add(userID, null);
                    return true;
                }

                return false;
            }
        }

        public bool LeaveGame(Guid userID)
        {
            return _players.Remove(userID);
        }

        public void ReadyStatus(Guid userID, bool status)
        {
            _players[userID].IsReady = status;
        }

        public void StartGame(Guid userID)
        {
            if (_players.Values.All(x => x.IsReady))
            {
                // TODO: Старт игры
                throw new NotImplementedException("Греня должен реализовать эту херню, все вопросы к нему");
            }
        }

        public void SendMaze(Guid userID, IMazeStructure structure)
        {
            // TODO: Валидация модели
            if (true)
            {
                _players[userID].Maze = new Maze<T>(structure, _endGameAction);
            }

            if(_players.Values.All(x=>x.Maze != null))
            {

            }
        }



        public bool MoveObject(Guid userID, Guid objectID, Direction direction)
        {
            var maze = _players[userID].Maze;
            return maze.MoveLiveGameObject(objectID, direction);
        }
    }
}
