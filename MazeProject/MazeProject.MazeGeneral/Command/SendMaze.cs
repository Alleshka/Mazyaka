using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Maze;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class SendMazeRequest : AbstractRequest
    {
        [DataMember]
        public Maze.Maze Maze { get; set; }

        [DataMember]
        public Guid UserID { get; set; }

        [DataMember]
        public Guid GameID { get; set; }

        public SendMazeRequest(Guid userID, Guid gameID, Maze.Maze maze = null) : base()
        {
            Maze = maze;
            UserID = userID;
            GameID = gameID;
        }
    }

    [DataContract]
    public class SendMazeResponse : AbstractResponse
    {
        public SendMazeResponse() : base()
        {
        }
    }
}
