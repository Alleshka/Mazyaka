using Maze.Common.MazePackages;
using Maze.Server.UdpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.MazeService.MessageSender
{
    public class UdpDataExchangeMessageSender : IMessageSenderService
    {
        private MazeUdpDataExchange _dataExchanger;

        public UdpDataExchangeMessageSender(MazeUdpDataExchange dataExchanger)
        {
            _dataExchanger = dataExchanger;
        }

        public void SendMessage(IMazePackage package, IPEndPoint endPoint)
        {
            _dataExchanger.SendMessage(package, endPoint);
        }
    }
}
