using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;
using MazeProject.Server.MessageSender;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActSendMaze : ActGameAbstract
    {
        public ActSendMaze(SendMazeRequest gameRequest, IGameService game, Sender sender) : base(gameRequest, game, sender)
        {

        }

        public override void Execute()
        {
            Guid userID = (request as SendMazeRequest).UserID;
            Guid gameID = (request as SendMazeRequest).GameID;
            Maze maze = (request as SendMazeRequest).Maze;
            gameService.FindGameByID(gameID).GameIsWaitPointEvent += GameService_GameIsWaitPointEvent;

            gameService.AddMaze(gameID, userID, maze);
        }

        private void GameService_GameIsWaitPointEvent(List<Guid> userList)
        {
            System.Diagnostics.Trace.WriteLine($"       ActSendMaze.GameService_GameIsWaitPointEvent - начало");
            sender.SendMessage(userList, new GivePoint());
            System.Diagnostics.Trace.WriteLine($"       ActSendMaze.GameService_GameIsWaitPointEvent - конец");
        }
    }
}
