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
        protected IUserService _userService;
        protected Dictionary<Guid, ILobby> _lobbyList = new Dictionary<Guid, ILobby>();

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

        public ILobby CreateGameByLobby(Guid lobbyID)
        {
            var lobby = _lobbyList[lobbyID];
            _lobbyList.Remove(lobbyID);
            return lobby;
        }

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
