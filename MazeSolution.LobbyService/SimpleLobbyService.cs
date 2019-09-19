using MazeSolution.Core.Models;
using MazeSolution.LoginService;
using System;
using System.Collections.Generic;
using System.Text;
// using System.Linq;

namespace MazeSolution.LobbyService
{
    public class SimpleLobbyService<T>: ILobbyService where T : ILobby, new()
    {
        /// <summary>
        /// Сервис пользователей
        /// </summary>
        protected IUserService _userService;

        /// <summary>
        /// Справочник лобби
        /// </summary>
        protected Dictionary<Guid, ILobby> _lobbyList = new Dictionary<Guid, ILobby>();

        /// <summary>
        /// Создать лобби
        /// </summary>
        /// <param name="user">Пользователь-хост</param>
        /// <returns>True если успешно создано, иначе false</returns>
        public bool Create(UserServiceModel user)
        {
            if (_userService.CheckUserSecurityKey(user))
            {
                var newLobby = new T();
                newLobby.AddPlayer(new UserLobbyModel(user));
                _lobbyList.Add(newLobby.ObjectID, newLobby);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Создать игру из лобби
        /// </summary>
        /// <param name="lobbyID">ID лобби</param>
        /// <returns>Лобби</returns>
        public ILobby CreateGameByLobby(Guid lobbyID)
        {
            var lobby = _lobbyList[lobbyID];
            _lobbyList.Remove(lobbyID);
            return lobby;
        }

        /// <summary>
        /// Присоедениться к лобби
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="lobbyID">ID лобби</param>
        /// <returns>True если подключение прошло успешно, иначе false</returns>
        public bool Join(UserServiceModel user, Guid lobbyID)
        {
            if(_userService.CheckUserSecurityKey(user))
            {
                if(_lobbyList.TryGetValue(user.ObjectID, out ILobby lobby))
                {
                    return lobby.AddPlayer(new UserLobbyModel(user));
                }
            }
            return false;
        }

        /// <summary>
        /// Покинуть лобби
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="lobbyID">ID лобби<</param>
        /// <returns>True если успешно покинул лобби, иначе false</returns>
        public bool Leave(UserServiceModel user, Guid lobbyID)
        {
            if (_userService.CheckUserSecurityKey(user))
            {
                if (_lobbyList.TryGetValue(user.ObjectID, out ILobby lobby))
                {
                    return lobby.RemovePlayer(user.ObjectID);
                }
            }
            return false;
        }
    }
}
