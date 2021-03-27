using Maze.Common;
using Maze.Common.Logging;
using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.MazeService.SessionService
{
    public class DumpSessionService : ISessionService
    {
        private Dictionary<string, MazeUser> _sessions = new Dictionary<string, MazeUser>();

        public DumpSessionService()
        {

        }

        public string AddUserSession(MazeUser user)
        {
            return MazeLogManager.Instance.Write($"Добавление пользователя {user.Login} в активные сессии", () =>
            {
                var token = Guid.NewGuid().ToString();
                _sessions.Add(token, user);
                MazeLogManager.Instance.Write($"Текущие сессии: {this}", Constants.Loggers.CommonLogger);
                return token;
            }, Constants.Loggers.CommonLogger);
        }

        public void DeleteSession(string userToken)
        {
            MazeLogManager.Instance.Write($"Удаление пользователя с токеном {userToken} из активных сессий", () =>
            {
                _sessions.Remove(userToken);
                Console.WriteLine(this);
            });
        }

        public MazeUser GetUserByTokenOrNull(string userToken)
        {
            _sessions.TryGetValue(userToken, out var user);
            return user;
        }

        public MazeUserRole GetUserRoleOrDefault(string userToken)
        {
            if (String.IsNullOrEmpty(userToken)) return MazeUserRole.Guest;
            else
            {
                _sessions.TryGetValue(userToken, out var user);          
                return user?.Role ?? MazeUserRole.Guest;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Активных пользователей: {_sessions.Count}");
            foreach (var user in _sessions)
            {
                builder.AppendLine($"{user.Key} - {user.Value.Login}");
            }

            return builder.ToString();
        }
    }
}
