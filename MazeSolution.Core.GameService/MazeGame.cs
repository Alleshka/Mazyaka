using MazeSolution.Core.GameObjects;
using MazeSolution.Core.GameObjects.LiveGameObject;
using MazeSolution.Core.GameService;
using MazeSolution.Core.MazeStructrure;
using MazeSolution.Core.Models;
using MazeSolution.LoginService;
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
        public Maze<T> Maze { get; set; }
        public Point StartPoint { get; set; }
        public BaseGameObject Character { get; set; }
    }

    public abstract class BaseMazeGame<T> : BaseMazeObject where T : ICell, new()
    {
        private IUserService _userService;

        public GameStatus Status { get; protected set; }

        protected readonly Dictionary<Guid, PlayerInfo<T>> _players;

        private readonly object _locker = new object();

        protected IActionStorage _actionStorage;

        public BaseMazeGame(IActionStorage actionStorage)
        {
            _players = new Dictionary<Guid, PlayerInfo<T>>();
            Status = GameStatus.Lobby;
            _actionStorage = actionStorage;
        }

        public void LobbyFormed()
        {
            //if (_players.Values.All(x => x.IsReady))
            //{
            //    Status = GameStatus.SendingMaze;
            //    _actionStorage.LobbyFormed();
            //}
        }

        public virtual bool SendMaze(UserServiceModel user, IMazeStructure structure, Point startPoint)
        {
            // TODO: Валидация модели
            if (_userService.CheckUserSecurityKey(user))
            {
                if (ValidateMaze(structure))
                {
                    lock (_locker)
                    {
                        var player = _players.FirstOrDefault(x => x.Value.Maze == null && x.Key != user.ObjectID);
                        if (player.Value != null) player.Value.Maze = new Maze<T>(structure, _actionStorage);
                    }

                    _players[user.ObjectID].StartPoint = startPoint;

                    if (_players.Values.All(x => x.Maze != null))
                    {
                        StartGame();
                    }

                    return true;
                }
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

        public bool MoveUserCharacter(UserServiceModel user, Direction direction)
        {
            if (_userService.CheckUserSecurityKey(user))
            {
                var player = _players[user.ObjectID];
                var maze = player.Maze;
                var character = player.Character;
                return maze.MoveLiveGameObject(character.ObjectID, direction);
            }

            return false;
        }

        protected bool ValidateMaze(IMazeStructure structure)
        {
            // TODO: Валидация лабиринта
            return true;
        }
    }
}
