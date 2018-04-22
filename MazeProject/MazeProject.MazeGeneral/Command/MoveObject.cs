using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class MoveObjectRequest : AbstractRequest
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public Guid GameID { get; set; }
        [DataMember]
        public MoveDirection Direction { get; set; }

        public MoveObjectRequest(Guid userID, Guid gameID, MoveDirection direction) : base()
        {
            UserID = userID;
            GameID = gameID;
            Direction = direction;
        }
    }


    [DataContract]
    public class MoveObjectResponse : AbstractResponse
    {
        [DataMember]
        public bool? IsMoved { get; set; }
        // TODO : Возможно, стоит возвращать позицию

        public MoveObjectResponse(bool? moved) : base()
        {
            IsMoved = moved;
        }
    }
}
