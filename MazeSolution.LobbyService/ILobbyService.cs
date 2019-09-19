using MazeSolution.Core.Models;
using System;

namespace MazeSolution.LobbyService
{
    /// <summary>
    /// Интерфейс сервиса игрового лобби
    /// </summary>
    public interface ILobbyService
    {
        /// <summary>
        /// Создать лобби
        /// </summary>
        /// <param name="user">Пользователь-хост</param>
        /// <returns>True если успешно создано, иначе false</returns>
        bool Create(UserServiceModel user);

        /// <summary>
        /// Присоедениться к лобби
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="lobbyID">ID лобби</param>
        /// <returns>True если подключение прошло успешно, иначе false</returns>
        bool Join(UserServiceModel user, Guid lobbyID);

        /// <summary>
        /// Покинуть лобби
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="lobbyID">ID лобби<</param>
        /// <returns>True если успешно покинул лобби, иначе false</returns>
        bool Leave(UserServiceModel user, Guid lobbyID);

        /// <summary>
        /// Создать игру из лобби
        /// </summary>
        /// <param name="lobbyID">ID лобби</param>
        /// <returns>Лобби</returns>
        ILobby CreateGameByLobby(Guid lobbyID);
    }
}