using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.General;

namespace MazeProject.GameService
{
    public class GameServiceFirst : AbstractGameService
    {
        public GameServiceFirst(IManager<MazeLobby> manager)
        {
            this.lobbyManager = manager;
        }

        public override Guid CreateGame(Guid userID)
        {
            MazeLobby lobby = new MazeLobby();
            lobby.AddPlayer(userID);
            this.lobbyManager.Add(lobby);
            return lobby.LobbyID;
        }

        public override Guid JoinGame(Guid userID, Guid gameID)
        {
            var lobby = this.lobbyManager[gameID];
            lobby.AddPlayer(userID);
            return lobby.LobbyID;
        }

        public void StartGame(Guid gameID)
        {

        }
    }
}
