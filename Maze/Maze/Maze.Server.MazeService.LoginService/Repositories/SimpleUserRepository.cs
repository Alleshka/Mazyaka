using Maze.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.Repositories
{
    public class SimpleUserRepository : IUserService
    {
        private Dictionary<Guid, MazeUser> _users = new Dictionary<Guid, MazeUser>();

        public MazeUser CreateUser(MazeUser user)
        {
            _users[user.ObjectID] = user;

            Console.WriteLine($"User created {user.Login}");
            Console.WriteLine(this);

            return user;
        }

        public MazeUser GetUserByID(Guid id)
        {
            _users.TryGetValue(id, out var user);
            return user;
        }

        public MazeUser GetUserByLogin(string userLogin)
        {
            return _users.Values.FirstOrDefault(x => x.Login == userLogin);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Всего пользователей: {_users.Count}");
            foreach (var user in _users)
            {
                builder.AppendLine($"{user.Key} - {user.Value.Login}");
            }

            return builder.ToString();
        }
    }
}
