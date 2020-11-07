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
        private Dictionary<string, MazeUser> _sessions = new Dictionary<string, MazeUser>();

        public string AddUserSession(MazeUser user)
        {
            var token = Guid.NewGuid().ToString();
            _sessions.Add(token, user);
            return token;
        }

        public void DeleteSession(string userToken)
        {
            _sessions.Remove(userToken);
        }

        public MazeUser GetUserByLoginOrNull(string userLogin)
        {
            return _sessions.FirstOrDefault(x => x.Value.Login == userLogin).Value;
        }

        public MazeUser GetUserByTokenOrNull(string userToken)
        {
            _sessions.TryGetValue(userToken, out var user);
            return user;
        }

        public MazeUserRole GetUserRoleOrNull(string userToken)
        {
            _sessions.TryGetValue(userToken, out var user);
            return user?.Role;
        }
    }
}
