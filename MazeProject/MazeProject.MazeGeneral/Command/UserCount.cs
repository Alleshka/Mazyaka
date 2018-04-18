using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class UserCountRequest : AbstractRequest
    {
        [DataMember]
        public Guid UserID { get; set; }
        public UserCountRequest(Guid userId) : base()
        {
            UserID = userId;
        }
    }

    [DataContract]
    public class UserCountResponse : AbstractResponse
    {
        [DataMember]
        public int UserCount { get; set; }
        public UserCountResponse(int count) : base()
        {
            UserCount = count;
        }
    }
}
