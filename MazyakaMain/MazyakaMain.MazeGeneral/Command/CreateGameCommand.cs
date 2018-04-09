using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazyakaMain.MazeGeneral.Command
{
    public class CreateGameRequest : AbstractPackage
    {
        public Guid UserID { get; set; } 

        public CreateGameRequest(Guid userId) : base()
        {
            UserID = userId;
        }
    }

    public class CreateGameResponse : AbstractPackage
    {
        public Guid GameID { get; set; }

        public CreateGameResponse(Guid gameID) : base()
        {
            GameID = gameID;
        }
    }
}
