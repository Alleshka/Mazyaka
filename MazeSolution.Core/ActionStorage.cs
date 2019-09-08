using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core
{
    public interface ILobbyActionStorage
    {
        Action LobbyFormed { get; }

        Action<Guid> UserJoined { get; }
        Action<Guid> UserLeft { get; }

        Action<Guid, bool> UserSetReadyStatus { get; }

        Action GameStart { get; }
    }

    public interface IGameActionStorage
    {
        Action<BaseMazeObject> EndGameAction { get; }
    }

    public interface IActionStorage : ILobbyActionStorage, IGameActionStorage
    {

    }

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
