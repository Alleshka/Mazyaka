using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    [DataContract]
    public class IsMyStepRequest : AbstractRequest
    {
        // TODO: Надо бы уже вынести userID в абстрактный
        [DataMember]
        public Guid UserID { get; set; }

        public IsMyStepRequest(Guid userID) : base ()
        {
            UserID = userID;
        }
    }

    [DataContract]
    public class IsMyStepResponse : AbstractResponse
    {
        [DataMember]
        public bool IsYourStep;
        public IsMyStepResponse(bool step) : base ()
        {
            IsYourStep = step;
        }
    }
}
