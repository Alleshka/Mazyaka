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
    public class ActMoveObject : ActGameAbstract
    {
        public ActMoveObject(MoveObjectRequest gameRequest, IGameService game, Sender sender) : base(gameRequest, game, sender)
        {

        }

        public override void Execute()
        {
            MoveObjectRequest moveObjectRequest = request as MoveObjectRequest;
            bool IsMoved = gameService.MoveObject(moveObjectRequest.GameID, moveObjectRequest.UserID, moveObjectRequest.Direction);
            response = new MoveObjectResponse(IsMoved);

            SendMessage(moveObjectRequest.UserID);

            Guid curUser = gameService.StepByUser(moveObjectRequest.GameID);
            SendMessage(curUser, new YourStep()); // Шлём другому пользователю, что может ходить
        }
    }
}