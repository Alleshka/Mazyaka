using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActMoveObject : ActGameAbstract
    {
        public ActMoveObject(MoveObjectRequest gameRequest, IGameService game) : base(gameRequest, game)
        {

        }

        public override void Execute()
        {
            MoveObjectRequest moveObjectRequest = request as MoveObjectRequest;
            bool IsMoved = gameService.MoveObject(moveObjectRequest.GameID, moveObjectRequest.UserID, moveObjectRequest.Direction);
            response = new MoveObjectResponse(IsMoved);

            MessageSender.Sender.GetInstanse().SendMessage(moveObjectRequest.UserID, response); // Шлём пользователю ответ на его ход

            Guid curUser = gameService.StepByUser(moveObjectRequest.GameID);
            MessageSender.Sender.GetInstanse().SendMessage(curUser, new YourStep()); // Шлём другому пользователю, что может ходить
        }
    }
}