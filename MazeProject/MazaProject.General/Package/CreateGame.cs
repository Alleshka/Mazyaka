using System;
using System.Collections.Generic;
using System.Text;

namespace MazaProject.General.Package
{
    public class CreateGameRequest : BaseRequest
    {
        public Guid CreatorID { get; set; }
        
        public CreateGameRequest(Guid creatorID) : base ("CreateGame")
        {
            this.CreatorID = creatorID;
        }
    }

    public class CreateGameResponse : BaseResponse
    {
        public Guid GameID { get; private set; }

        public CreateGameResponse(Guid gameID) : base ("CreateGame")
        {
            this.GameID = gameID;
        }
    }
}
