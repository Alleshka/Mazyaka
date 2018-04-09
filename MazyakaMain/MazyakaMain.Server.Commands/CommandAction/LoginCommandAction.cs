using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazyakaMain.MazeGeneral;

namespace MazyakaMain.Server.Commands
{
    public class LoginCommandAction : AbstractCommandAction
    {
        public LoginCommandAction(LoginRequest command) : base(command)
        {
        
        }

        public override void Execute()
        {
            LoginRequest command = response as LoginRequest;

            String login = command.Login;
            String password = command.Password;

            Guid id = Guid.NewGuid(); // TODO : Вынести всё отдельно

            this.response = new LoginResponse(id);
        }

        public override AbstractPackage GetResponse()
        {
            return this.response as LoginResponse;
        }

        public override void SendResponse()
        {
            throw new NotImplementedException();
        }
    }
}
