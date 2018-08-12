using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MazeProject.General.Package;
using MazeProject.CommandBuilder.Commands;
using MazeProject.GameService;
using MazeProject.LoginService;
using System.Net.Sockets;

namespace MazeProject.CommandBuilder
{
    public class CommandParser
    {
        private LoginService.ILoginService loginService;
        private MessageSender.MessageSender sender;

        public CommandParser(MessageSender.MessageSender sender)
        {
            loginService = new LoginService.FirstLoginService();

            this.sender = sender;
        }

        public ICommand Parse (byte[] bytes, Socket socket)
        {
            String jsonString = Encoding.UTF8.GetString(bytes);
            String packageType = (String)(JObject.Parse(jsonString))["TypePackage"];
            ICommand command = null;

            switch (packageType)
            {
                case "Login":
                    {
                        LoginRequest request = JsonConvert.DeserializeObject<LoginRequest>(jsonString);
                        command = new LoginCommand(request, loginService, sender, socket);
                        break;
                    }
                default:
                    {
                        command = null;
                        break;
                    }
            }

            return command;
        }
    }
}
