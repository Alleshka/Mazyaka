using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class GameListRequest : AbstractRequest
    {
        [DataMember]
        public Guid UserID { get; set; }

        public GameListRequest(Guid userID) : base()
        {
            UserID = userID;
        }
    }

    [DataContract]
    public class GameListResponse : AbstractResponse
    {
       [DataMember]
       public List<Guid> GameList { get; set; }

        public GameListResponse(List<Guid> list) : base()
        {
            GameList = list;
        }
    }
}
