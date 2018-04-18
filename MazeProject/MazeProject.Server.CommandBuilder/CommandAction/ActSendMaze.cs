using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActSendMaze : ActGameAbstract
    {
        public ActSendMaze(SendMazeRequest gameRequest, IGameService game) : base(gameRequest, game)
        {

        }

        public override void Execute()
        {
            Guid userID = (request as SendMazeRequest).UserID;
            Guid gameID = (request as SendMazeRequest).GameID;
            Maze maze = (request as SendMazeRequest).Maze;

            gameService.AddMaze(gameID, userID, maze);
            response = new SendMazeResponse();


            var sender = MessageSender.Sender.GetInstanse();
            sender.SendMessage(userID, response);

            GameRoom room = gameService.FindGameByID(gameID);
            if(room.Status== StatusGame.WAINPOINTS)
            {
                sender.SendMessage(room.PlayerList.Select(x => x.PlayerID).ToList(), new GivePoint());
            }
        }
    }
}
