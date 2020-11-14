using Maze.Common.DataExhange;
using Maze.Common.MazePackages;
using Maze.Server.ImplementationStorage;
using System.Net;

namespace Maze.Server.MazeService.MessageSenderService
{
    public class UdpDataExchangeMessageSender : IMessageSenderService
    {
        public void SendMessage(IMazePackage package, IPEndPoint endPoint)
        {
            MazeImplementationStorage.Instance.GetImplementation<IDataExchanger>().SendMessage(package, endPoint);
        }
    }
}
