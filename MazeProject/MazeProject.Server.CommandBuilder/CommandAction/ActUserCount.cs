using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Command;


namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActUserCount : ActAbstract
    {
        public ActUserCount(UserCountRequest command, MessageSender.Sender sender) : base(command, sender)
        {

        }

        public override void Execute()
        {
            UserCountRequest command = this.request as UserCountRequest;

            int count = sender.GetUserCount();

            UserCountResponse response = new UserCountResponse(count);
            sender.SendMessage(command.UserID, response);
        }
    }
}
