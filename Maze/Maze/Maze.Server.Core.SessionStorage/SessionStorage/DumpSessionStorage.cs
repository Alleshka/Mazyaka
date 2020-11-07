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

        private static DumpSessionStorage _instance;
        public static DumpSessionStorage Instance
        {
            get
            {
                return _instance ?? (_instance = new DumpSessionStorage());
            }
        }

        private DumpSessionStorage()
        {

        }


        public string AddUserSession(MazeUser user)
        {
            var token = Guid.NewGuid().ToString();
            _sessions.Add(token, user);

            Console.WriteLine(this);

            return token;
        }

        public void DeleteSession(string userToken)
        {
            _sessions.Remove(userToken);
            Console.WriteLine(this);
        }

        public MazeUser GetUserByLoginOrNull(string userLogin)
        {
            return _sessions.Values.FirstOrDefault(x => x.Login == userLogin);
        }

        public MazeUser GetUserByTokenOrNull(string userToken)
        {
            _sessions.TryGetValue(userToken, out var user);
            return user;
        }

        public MazeUserRole GetUserRoleOrNull(string userToken)
        {
            if (String.IsNullOrEmpty(userToken)) return new MazeUserRole(Constants.Roles.GUEST);
            else
            {
                _sessions.TryGetValue(userToken, out var user);
                return user?.Role;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Активных пользователей: {_sessions.Count}");
            foreach(var user in _sessions)
            {
                builder.AppendLine($"{user.Key} - {user.Value.Login}");
            }

            return builder.ToString();
        }
    }
}
