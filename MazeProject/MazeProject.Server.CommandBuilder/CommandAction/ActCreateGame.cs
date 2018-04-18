using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActCreateGame : ActGameAbstract
    {
        public ActCreateGame(CreateGameRequest gameRequest, IGameService game) : base(gameRequest, game)
        {

        }

        public override void Execute()
        {
            Guid userID = (request as CreateGameRequest).UserID;
            Guid gameID = this.gameService.CreateGame(userID);
            response = new CreateGameResponse(gameID);

            MessageSender.Sender.GetInstanse().SendMessage(userID, response); // Отправляем клиенту ID игры
        }
    }
}
