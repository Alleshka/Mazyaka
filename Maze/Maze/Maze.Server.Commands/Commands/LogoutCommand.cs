using Maze.Common.MazePackages;
using Maze.Common.MazePackages.MazePackages;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionService;
using System;

namespace Maze.Server.Commands.Commands
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
            MazeServiceStorage.Instance.GetService<ISessionService>().DeleteSession(_userToken);
            return PackageFactory.MessageCommon("bue");
        }
    }
}
