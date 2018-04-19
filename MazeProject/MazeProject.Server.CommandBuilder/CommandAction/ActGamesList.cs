using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.Server.MessageSender;
using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActGamesList : ActGameAbstract
    {
        public ActGamesList(GameListRequest gameRequest, IGameService game, Sender sender) : base(gameRequest, game, sender)
        {

        }

        public override void Execute()
        {
            GameListRequest listRequest = request as GameListRequest;
            response = new GameListResponse(gameService.GamesList());
            SendMessage(listRequest.UserID);
        }
    }
}