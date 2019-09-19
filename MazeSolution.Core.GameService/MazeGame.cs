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
    /// <summary>
    /// Игровой статус
    /// </summary>
    public enum GameStatus
    {
        Lobby,
        SendingMaze,
        Gaming,
        Finished
    }

    /// <summary>
    /// Информация об игроке
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PlayerInfo<T> where T : ICell, new()
    {
        /// <summary>
        /// Лабиринт
        /// </summary>
        public Maze<T> Maze { get; set; }

        /// <summary>
        /// Точка старта
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Персонаж
        /// </summary>
        public BaseGameObject Character { get; set; }
    }

    /// <summary>
    /// Базовый класс для игры
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMazeGame<T> : BaseMazeObject where T : ICell, new()
    {
        /// <summary>
        /// Сервис пользователей
        /// </summary>
        private IUserService _userService;

        /// <summary>
        /// Статус игры
        /// </summary>
        public GameStatus Status { get; protected set; }

        /// <summary>
        /// Справочник игроков
        /// </summary>
        protected readonly Dictionary<Guid, PlayerInfo<T>> _players;

        /// <summary>
        /// Объект синхронизации
        /// </summary>
        private readonly object _locker = new object();

        /// <summary>
        /// Хранилище операций
        /// </summary>
        protected IActionStorage _actionStorage;

        /// <param name="actionStorage">Хранилище операций</param>
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

        /// <summary>
        /// Отправить лабиринт
        /// </summary>
        /// <param name="user">Пользователь, отправивший лабиринт</param>
        /// <param name="structure">Структура лабиринта</param>
        /// <param name="startPoint">Точка старта пользователя</param>
        /// <returns></returns>
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

        /// <summary>
        /// Начать игру
        /// </summary>
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

        /// <summary>
        /// Подвинуть персонажа пользователя
        /// </summary>
        /// <param name="user">Пользователь, персонаж которого будет подвинут</param>
        /// <param name="direction">Направление движения</param>
        /// <returns>True если операция прошла успешно, иначе false</returns>
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

        /// <summary>
        /// Валидирует структуру лабиринта
        /// </summary>
        /// <param name="structure">Структура лабиринта</param>
        /// <returns>True если структура валидна, иначе false</returns>
        protected bool ValidateMaze(IMazeStructure structure)
        {
            // TODO: Валидация лабиринта
            return true;
        }
    }
}
