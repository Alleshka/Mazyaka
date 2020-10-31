using Maze.Common;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.SessionStorage
{
    public class DumpSessionStorage : ISessionStorage
    {
        private IList<string> roles = Constants.Roles.ALL.ToList();
        private Random T = new Random();

        public string AddUserSession(MazeUser user)
        {
            throw new NotImplementedException();
        }

        public MazeUser GetUserByLoginOrNull(string userLogin)
        {
            throw new NotImplementedException();
        }

        public MazeUser GetUserByTokenOrNull(string userToken)
        {
            throw new NotImplementedException();
        }

        public MazeUserRole GetUserRoleOrNull(string userToken)
        {
            return new MazeUserRole(roles[T.Next(roles.Count)]);
        }
    }
}
