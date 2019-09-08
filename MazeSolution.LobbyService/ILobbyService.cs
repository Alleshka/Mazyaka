using MazeSolution.Core.Models;
using System;

namespace MazeSolution.LobbyService
{
    public interface ILobbyService
    {
        bool Create(UserServiceModel user);
        bool Join(UserServiceModel user, Guid lobbyID);
        bool Leave(UserServiceModel user, Guid lobbyID);
        ILobby CreateGameByLobby(Guid lobbyID);
    }
}