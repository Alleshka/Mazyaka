using MazeSolution.LobbyService;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameService
{
    /// <summary>
    /// Интерфейс игрового сервиса
    /// </summary>
    public interface IGameService
    {
        void StartGame(Guid lobbyID);
    }

    /// <summary>
    /// Базовая реализация игрового сервиса
    /// </summary>
    public class SimpleGameService : IGameService
    {
        private ILobbyService _lobbyService;

        public void StartGame(Guid lobbyID)
        {
            var lobby = _lobbyService.CreateGameByLobby(lobbyID);
            throw new NotImplementedException();
        }
    }
}
