using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General
{

    public class MazeLobby
    {
        public Guid LobbyID { get; private set; }

        public List<Player> PlayerList { get; private set; }

        public MazeLobby()
        {
            LobbyID = Guid.NewGuid();
            PlayerList = new List<Player>();
        }

        public bool AddPlayer(Guid userID)
        {
            return AddPlayer(new Player()
            {
                PlayerID = userID,
                //IsReady = false
            });
        }

        public bool AddPlayer(Player user)
        {
            PlayerList.Add(user);
            return true;
        }

        public bool RemovePlayer(Guid userID)
        {
            PlayerList.RemoveAll(x => x.PlayerID == userID);
            return true;
        }
    }
}
