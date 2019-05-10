using MazeSolution.Core.GameObjects;
using MazeSolution.Core.GameObjects.LiveGameObject;
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
        public Point StartPoint { get; set; }
        public BaseGameObject Character { get; set; }
    }

    public abstract class BaseMazeGame<T> : BaseMazeObject where T : ICell, new()
    {
        protected int MaxPlayers { get; set; }

        public GameStatus Status { get; protected set; }

        protected readonly Dictionary<Guid, PlayerInfo<T>> _players;

        private object _locker = new object();
        protected IActionStorage _actionStorage;

        public BaseMazeGame(IActionStorage actionStorage)
        {
            _players = new Dictionary<Guid, PlayerInfo<T>>();
            Status = GameStatus.Lobby;
            _actionStorage = actionStorage;
        }

        public bool JoinGame(Guid userID)
        {
            lock (_locker)
            {
                if (_players.Count < MaxPlayers)
                {
                    _players.Add(userID, null);
                    _actionStorage?.UserJoined(userID);
                    return true;
                }

                return false;
            }
        }

        public bool LeaveGame(Guid userID)
        {
            var result = _players.Remove(userID);
            if (result) _actionStorage?.UserLeft(userID);
            return result;
        }

        public void ReadyStatus(Guid userID, bool status)
        {
            _players[userID].IsReady = status;
            _actionStorage?.UserSetReadyStatus(userID, status);
        }

        public void LobbyFormed()
        {
            if (_players.Values.All(x => x.IsReady))
            {
                Status = GameStatus.SendingMaze;
                _actionStorage.LobbyFormed();
            }
        }

        public virtual bool SendMaze(Guid userID, IMazeStructure structure, Point startPoint)
        {
            // TODO: Валидация модели
            if (ValidateMaze(structure))
            {
                lock (_locker)
                {
                    var player = _players.FirstOrDefault(x => x.Value.Maze == null && x.Key != userID);
                    if (player.Value != null) player.Value.Maze = new Maze<T>(structure, _actionStorage);
                }

                _players[userID].StartPoint = startPoint;

                if (_players.Values.All(x => x.Maze != null))
                {
                    StartGame();
                }

                return true;
            }

            return false;
        }

        protected virtual void StartGame()
        {
            Status = GameStatus.Gaming;
            foreach(var player in _players.Values)
            {
                // TODO: Реализовать фабрику для получения нужного типа живого объекта
                var human = new Human();
                player.Character = human;
                player.Maze.AddLiveGameObject(human, player.StartPoint.Line, player.StartPoint.Column);
            }
            _actionStorage?.GameStart();
        }

        public bool MoveUserCharacter(Guid userID, Direction direction)
        {
            var player = _players[userID];
            var maze = player.Maze;
            var character = player.Character;
            return maze.MoveLiveGameObject(character.ObjectID, direction);
        }

        protected bool ValidateMaze(IMazeStructure structure)
        {
            // TODO: Валидация лабиринта
            return true;
        }
    }
}
