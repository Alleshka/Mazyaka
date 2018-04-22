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
    public class ActSendStartPoint : ActGameAbstract
    {
        public ActSendStartPoint(SendStartPositionRequest gameRequest, IGameService game, Sender sender) : base(gameRequest, game, sender)
        {

        }

        public override void Execute()
        {

            Guid userID = (request as SendStartPositionRequest).UserID;
            Guid gameID = (request as SendStartPositionRequest).GameID;
            var point = (request as SendStartPositionRequest).Point;

            gameService.FindGameByID(gameID).GameIsStartedEvent += GameService_GameIsStartedEvent;
            gameService.AddLive(gameID, userID, point);
        }

        private void GameService_GameIsStartedEvent(Guid firstUser)
        {
            AbstractMessage response = new YourStep();
            sender.SendMessage(firstUser, response); // Сообщаем человеку про его ход
        }
    }
}