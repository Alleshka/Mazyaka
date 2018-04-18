using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    /// <summary>
    /// Запрос для создания игровой комнаты
    /// </summary>
    [DataContract]
    public class CreateGameRequest : AbstractRequest
    {
        [DataMember]
        public Guid UserID { get; set; }

        public CreateGameRequest(Guid userID) : base()
        {
            UserID = userID;
        }
    }

    /// <summary>
    /// Ответ на запрос
    /// </summary>
    [DataContract]
    public class CreateGameResponse : AbstractResponse
    {
        [DataMember]
        public Guid GameID { get; set; }
        
        public CreateGameResponse(Guid gameID) : base()
        {
            GameID = gameID;
        }
    }
}
