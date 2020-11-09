using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using System.Net;

namespace Maze.Server.MazeService.MessageSender
{
    public interface IMessageSenderService : IMazeService
    {
        void SendMessage(IMazePackage package, IPEndPoint endPoint);
    }
}
