using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Command;
using System.Net.Sockets;
using MazeProject.Server.MessageSender;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    /// <summary>
    /// Действие при запросе на вход в системе
    /// </summary>
    public class ActLogin : ActAbstract
    {
        private System.Net.Sockets.Socket socket;
        private LoginService.ILoginService loginService;

        public ActLogin(LoginRequest request, Socket sock, LoginService.ILoginService login, MessageSender.Sender sender) : base(request, sender)
        {
            socket = sock;
            loginService = login;
        }

        public override void Execute()
        {
            LoginRequest request = this.request as LoginRequest; // Приводим к пакету
            Guid id = loginService.Login(request.Login, request.Password);
            this.response = new LoginResponse(id);

            sender.AddUser(id, socket); // Добавляем в список пользователей
            sender.SendMessage(id, this.response);
        }
    }
}
