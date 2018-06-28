using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.General.Package;
using MazeProject.LoginService;
using MazeProject.MessageSender;
using System.Net.Sockets;

namespace MazeProject.CommandBuilder.Commands
{
    public class LoginCommand : BaseCommand
    {
        private ILoginService loginService;
        private MessageSender.MessageSender messageSender;
        private Socket socket;

        public LoginCommand(BasePackage package, ILoginService loginService, MessageSender.MessageSender sender, Socket userSocket) : base(package)
        {
            this.loginService = loginService;
            this.messageSender = sender;
            this.socket = userSocket;
        }

        public override Tuple<List<Guid>, string> Execute()
        {
            LoginRequest loginRequest = this.Request as LoginRequest;
            Guid userID = loginService.Login(loginRequest.Login, loginRequest.Passwod);
            messageSender.AddUser(socket, userID);
            LoginResponse loginResponse = new LoginResponse(loginRequest.Login, userID);
            return new Tuple<List<Guid>, string>(new List<Guid>() { userID }, loginResponse.ToString());
        }
    }
}
