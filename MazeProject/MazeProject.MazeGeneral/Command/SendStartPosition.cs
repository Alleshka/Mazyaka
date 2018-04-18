using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MazeProject.MazeGeneral.Maze;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class SendStartPositionRequest : AbstractRequest 
    {
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public Guid GameID { get; set; }
        [DataMember]
        public MazePoint Point { get; set; }

        public SendStartPositionRequest(Guid userID, Guid gameID, MazePoint point) : base()
        {
            UserID = userID;
            GameID = gameID;
            Point = point;
        }
    }

    [DataContract]
    public class SendStartPositionResponce : AbstractResponse
    {
        public SendStartPositionResponce() : base()
        {

        }
    }
}
