using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General
{
    public class MazeLobby
    {
        public Guid LobbyID { get; private set; }

        public MazeLobby()
        {
            LobbyID = Guid.NewGuid();
        }
    }
}
