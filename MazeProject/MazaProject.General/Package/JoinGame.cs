using System;
using System.Collections.Generic;
using System.Text;

namespace MazaProject.General.Package
{
    public class JoinGameRequest : BaseRequest
    {
        public Guid GameID { get; private set; }
        public Guid UserID { get; private set; }

        public JoinGameRequest(Guid gameID, Guid userID) : base ("JoinGame")
        {
            this.GameID = gameID;
            this.UserID = userID;
        }
    }

    public class JoinGameResponse : BaseResponse
    {
        public bool IsJoined { get; private set; }

        public JoinGameResponse(bool isJoined) : base ("JoinGame")
        {
            IsJoined = isJoined;
        }
    }
}
