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
    public class ActJoinGame : ActGameAbstract
    {
        public ActJoinGame(JoinGameRequest gameRequest, IGameService game, Sender sender) : base(gameRequest, game, sender)
        {
            gameService.HasUserConnectedEvent += GameService_HasUserConnectedEvent;
            gameService.GameIsWaitMazeEvent += GameService_GameIsWaitMazeEvent;
        }

        public override void Execute()
        {
            JoinGameRequest joinGame = request as JoinGameRequest;
            gameService.JoinGame(joinGame.UserID, joinGame.GameID); // Получаем ответ о статусе входа
        }

        private void GameService_GameIsWaitMazeEvent(List<Guid> userList)
        {
            sender.SendMessage(userList, new GiveMaze()); // сообщаем о готовности принять лабиринт
        }

        private void GameService_HasUserConnectedEvent(Guid id, bool status)
        {
            // Отправляем пользователю ответ о присоединении игры
            this.response = new JoinGameResponse(status);
            SendMessage(id);
        }
    }
}
