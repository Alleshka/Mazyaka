using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazyakaMain.MazeGeneral;
using MazyakaMain.MazeGeneral.Command;

namespace MazyakaMain.Server.Commands
{
    public class CreateGameCommandAction : AbstractCommandAction
    {
        public CreateGameCommandAction(CreateGameRequest creategame) : base(creategame)
        {

        }

        public override void Execute()
        {
            CreateGameRequest request = this.request as CreateGameRequest;

            // TODO: Происходит создание игры
            Guid gameID = Guid.NewGuid();

            this.response = new CreateGameResponse(gameID);
        }

        public override AbstractPackage GetResponse()
        {
            throw new NotImplementedException();
        }

        public override void SendResponse()
        {
            throw new NotImplementedException();
        }
    }
}
