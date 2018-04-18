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

        public CommandParser()
        {
            loginService = new LoginService.LoginService();
            gameService = new GameService.GameServiceFirst();
        }

        public ActAbstract ParseCommand(byte[] bytes, Socket socket)
        {
            var command = AbstractRequest.ToObject(bytes);

            if (command is CreateGameRequest) return new ActCreateGame(command as CreateGameRequest, gameService);
            if (command is GameListRequest) return new ActGamesList(command as GameListRequest, gameService);
            if (command is JoinGameRequest) return new ActJoinGame(command as JoinGameRequest, gameService);
            if (command is LoginRequest) return new ActLogin(command as LoginRequest, socket, loginService);
            if (command is MoveObjectRequest) return new ActMoveObject(command as MoveObjectRequest, gameService);
            if (command is SendMazeRequest) return new ActSendMaze(command as SendMazeRequest, gameService);
            if (command is SendStartPositionRequest) return new ActSendStartPoint(command as SendStartPositionRequest, gameService);
            if (command is UserCountRequest) return new ActUserCount(command as UserCountRequest);

            return null;
        }
    }
}
