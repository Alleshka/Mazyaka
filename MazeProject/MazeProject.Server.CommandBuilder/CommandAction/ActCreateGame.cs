using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;
using MazeProject.Server.MessageSender;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActCreateGame : ActGameAbstract
    {
        public ActCreateGame(CreateGameRequest gameRequest, IGameService game, Sender sender) : base(gameRequest, game, sender)
        {

        }

        public override void Execute()
        {
            Guid userID = (request as CreateGameRequest).UserID;
            Guid gameID = this.gameService.CreateGame(userID);
            response = new CreateGameResponse(gameID);

            SendMessage(userID); // Отправляем сформированный ответ
        }
    }
}
