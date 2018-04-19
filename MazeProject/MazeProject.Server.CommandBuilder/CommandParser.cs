using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.Server.CommandBuilder.CommandAction;
using MazeProject.MazeGeneral.Command;
using System.Net.Sockets;
using MazeProject.Server.LoginService;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder
{
    public class CommandParser : ICommandParser
    {
        private ILoginService loginService;
        private IGameService gameService;

        private MessageSender.Sender sender;

        public CommandParser(MessageSender.Sender sndr)
        {
            loginService = new LoginService.LoginService();
            gameService = new GameService.GameServiceFirst();
            sender = sndr;
        }

        public ActAbstract ParseCommand(byte[] bytes, Socket socket)
        {
            var command = AbstractRequest.ToObject(bytes);

            ActAbstract act = null;

            if (command is CreateGameRequest)
            {
                act = new ActCreateGame(command as CreateGameRequest, gameService, sender);
            }
            else if (command is GameListRequest)
            {
                act = new ActGamesList(command as GameListRequest, gameService, sender);
            }
            else if (command is JoinGameRequest)
            {
                act = new ActJoinGame(command as JoinGameRequest, gameService, sender);
            }
            else if (command is LoginRequest)
            {
                act = new ActLogin(command as LoginRequest, socket, loginService, sender);
            }
            else if (command is MoveObjectRequest)
            {
                act = new ActMoveObject(command as MoveObjectRequest, gameService, sender);
            }
            else if (command is SendMazeRequest)
            {
                act = new ActSendMaze(command as SendMazeRequest, gameService, sender);
            }
            else if (command is SendStartPositionRequest)
            {
                act = new ActSendStartPoint(command as SendStartPositionRequest, gameService, sender);
            }
            else if (command is UserCountRequest)
            {
                act = new ActUserCount(command as UserCountRequest, sender);
            }

            return act;
        }


    }
}
