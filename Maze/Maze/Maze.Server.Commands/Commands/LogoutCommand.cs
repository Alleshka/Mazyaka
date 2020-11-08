using Maze.Common.MazePackages;
using Maze.Server.Core.ServiceStorage;
using Maze.Server.Core.SessionStorage;
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

        public override IMazePackage Execute()
        {
            MazeServiceStorage.Instance.GetService<ISessionStorage>().DeleteSession(_userToken);
            return PackageFactory.MessageCommon("bue");
        }
    }
}
