using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core
{
    /// <summary>
    /// Интерфейс хранилища операций лобби
    /// </summary>
    public interface ILobbyActionStorage
    {
        Action LobbyFormed { get; }

        Action<Guid> UserJoined { get; }
        Action<Guid> UserLeft { get; }

        Action<Guid, bool> UserSetReadyStatus { get; }

        Action GameStart { get; }
    }

    /// <summary>
    /// Интерфейс хранилищая операций игры
    /// </summary>
    public interface IGameActionStorage
    {
        Action<BaseMazeObject> EndGameAction { get; }
    }

    /// <summary>
    /// Интерфейс общего хранилища операций
    /// </summary>
    public interface IActionStorage : ILobbyActionStorage, IGameActionStorage
    {

    }

    /// <summary>
    /// Базовая реализация хранилища операций
    /// </summary>
    public class DefaultActionStorage : IActionStorage
    {
        public DefaultActionStorage()
        {

        }

        public Action LobbyFormed { get; set; }

        public Action<BaseMazeObject> EndGameAction { get; set; }

        public Action<Guid> UserJoined { get; set; }

        public Action<Guid> UserLeft { get; set; }

        public Action GameStart { get; set; }

        public Action<Guid, bool> UserSetReadyStatus { get; set; }
    }
}
