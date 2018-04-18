using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActJoinGame : ActGameAbstract
    {
        public ActJoinGame(JoinGameRequest gameRequest, IGameService game) : base(gameRequest, game)
        {

        }

        public override void Execute()
        {
            JoinGameRequest joinGame = request as JoinGameRequest;

            bool accepted = gameService.JoinGame(joinGame.UserID, joinGame.GameID); // Получаем ответ о статусе входа

            this.response = new JoinGameResponse(accepted);

            var sender = MessageSender.Sender.GetInstanse();
            sender.SendMessage(joinGame.UserID, response); // Отправляем пользователю ответ

            
            // Если игроков хватает
            GameRoom room = gameService.FindGameByID(joinGame.GameID);
            if(room.Status==StatusGame.WAITMAZES)
            {
                sender.SendMessage(room.PlayerList.Select(x=>x.PlayerID).ToList(), new GiveMaze()); // Отправляем пользователям, что готовы принять лабиринты
            }
        }
    }
}
