using MazeSolution.Core;
using MazeSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.LobbyService
{
    /// <summary>
    /// Интерфейс лобби
    /// </summary>
    public interface ILobby : IBaseMazeObject
    {
        /// <summary>
        /// Количество игроков
        /// </summary>
        int PlayersCount { get; set; }

        /// <summary>
        /// Добавить пользователя в лобби
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>True если успешно добавлен, иначе false</returns>
        bool AddPlayer(UserLobbyModel user);

        /// <summary>
        /// Удалить пользователя из лобби
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <returns>True, если успешно удален, иначе false</returns>
        bool RemovePlayer(Guid userID);

        /// <summary>
        /// Установить готовность пользователя
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="readyStatus">Статус готовности</param>
        void SetPlayerReady(Guid userID, bool readyStatus);
    }

    public class SimpleLobby : BaseMazeObject, ILobby
    {
        public Dictionary<Guid, UserLobbyModel> Players { get; }

        /// <summary>
        /// Количество игроков
        /// </summary>
        public int PlayersCount { get; set; }

        /// <summary>
        /// Объект синхронизации
        /// </summary>
        private readonly object _locker = new object();

        public SimpleLobby()
        {
            PlayersCount = 2;
            Players = new Dictionary<Guid, UserLobbyModel>();
        }

        /// <summary>
        /// Установить готовность пользователя
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="readyStatus">Статус готовности</param>
        public void SetPlayerReady(Guid userID, bool readyStatus)
        {
            Players[userID].IsReady = readyStatus;
        }

        /// <summary>
        /// Добавить пользователя в лобби
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>True если успешно добавлен, иначе false</returns>
        public bool AddPlayer(UserLobbyModel user)
        {
            lock (_locker)
            {
                if (Players.Count < PlayersCount)
                {
                    Players.Add(user.ObjectID, user);
                }
            }
            return false;
        }

        /// <summary>
        /// Удалить пользователя из лобби
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <returns>True, если успешно удален, иначе false</returns>
        public bool RemovePlayer(Guid user)
        {
            return Players.Remove(user);
        }
    }
}
