using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class JoinGameRequest : AbstractRequest
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public Guid GameID;

        public JoinGameRequest(Guid user, Guid game) : base()
        {
            UserID = user;
            GameID = game;
        }
    }

    [DataContract]
    public class JoinGameResponse : AbstractResponse
    {
        [DataMember]
        public bool IsAccepted { get; set; }
        
        public JoinGameResponse(bool assepted) : base ()
        {
            IsAccepted = assepted;
        }
    }
}
