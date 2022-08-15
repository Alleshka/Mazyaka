using Maze.Common.MazePackages;
using Maze.Server.Common;
using Maze.Server.MazeService.SessionService;
using System;

namespace Maze.Server.MazeCommands.MazeCommands
{
    class LogoutCommand : BaseCommand
    {
        private String _userToken;

        public LogoutCommand(String userToken)
        {
            _userToken = userToken;
        }

        protected override IMazePackage ExecuteCommand()
        {
            MazeDIContaner.Get<ISessionService>().DeleteSession(_userToken);
            return PackageFactory.MessageCommon("bue");
        }
    }
}
