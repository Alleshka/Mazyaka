using Maze.Common;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.Core.SessionService
{
    public class DumpSessionService : ISessionService
    {
        private Dictionary<string, MazeUser> _sessions = new Dictionary<string, MazeUser>();

        public DumpSessionService()
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
