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
            var room = gameService.FindGameByID(moveObjectRequest.GameID);

            bool? IsMoved = gameService.MoveObject(moveObjectRequest.GameID, moveObjectRequest.UserID, moveObjectRequest.Direction);

            if (room.Status != StatusGame.FINISHED)
            {
                response = new MoveObjectResponse(IsMoved);
                SendMessage(moveObjectRequest.UserID);

                Guid curUser = gameService.CurUser(moveObjectRequest.GameID);
                SendMessage(curUser, new YourStep()); // Шлём другому пользователю, что может ходить
            }
            else
            {
                // TODO: Можно сделать два разных пакета для победителя и проигравшего
                response = new GameFinished(moveObjectRequest.UserID);
                SendMessage(room.GetUsersID, response);
            }
        }
    }
}