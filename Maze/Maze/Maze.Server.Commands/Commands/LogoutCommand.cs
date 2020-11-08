using Maze.Common.MazePackages;
using Maze.Server.Core.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Commands.Commands
{
    class LogoutCommand : BaseCommand
    {
        private ISessionStorage _sessionStorage;
        private String _userToken;

        public LogoutCommand(ISessionStorage storage, String userToken)
        {
            _sessionStorage = storage;
            _userToken = userToken;
        }

        public override IMazePackage Execute()
        {
            _sessionStorage.DeleteSession(_userToken);
            return PackageFactory.MessageCommon("bue");
        }
    }
}
