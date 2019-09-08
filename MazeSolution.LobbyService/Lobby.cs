using MazeSolution.Core;
using MazeSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.LobbyService
{
    public interface ILobby : IBaseMazeObject
    {
        int PlayersCount { get; set; }
        bool AddPlayer(UserLobbyModel user);
        bool RemovePlayer(Guid userID);
        void SetUserReady(Guid userID, bool readyStatus);
    }

    public class SimpleLobby : BaseMazeObject, ILobby
    {
        public Dictionary<Guid, UserLobbyModel> Players { get; }

        public int PlayersCount { get; set; }

        private readonly object _locker = new object();

        public SimpleLobby()
        {
            PlayersCount = 2;
            Players = new Dictionary<Guid, UserLobbyModel>();
        }

        public void SetUserReady(Guid userID, bool readyStatus)
        {
            Players[userID].IsReady = readyStatus;
        }

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

        public bool RemovePlayer(Guid user)
        {
            return Players.Remove(user);
        }
    }
}
