using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActGamesList : ActGameAbstract
    {
        public ActGamesList(GameListRequest gameRequest, IGameService game) : base(gameRequest, game)
        {

        }

        public override void Execute()
        {
            GameListRequest listRequest = request as GameListRequest;
            response = new GameListResponse(gameService.GamesList());
            MessageSender.Sender.GetInstanse().SendMessage(listRequest.UserID, response);
        }
    }
}