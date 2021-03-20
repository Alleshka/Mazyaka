using Maze.Common.DataExhange;
using Maze.Common.MazePackages;
using Maze.Server.AutofacContainer;
using System.Net;

namespace Maze.Server.MazeService.MessageSenderService
{
    public class UdpDataExchangeMessageSender : IMessageSenderService
    {
        public void SendMessage(IMazePackage package, IPEndPoint endPoint)
        {
            var exchanger = MazeAutofacContainer.Instance.GetImplementation<IDataExchanger>();
            exchanger.SendMessage(package, endPoint);
        }
    }
}
