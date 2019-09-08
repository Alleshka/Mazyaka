using MazeSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.LoginService
{
    public interface IUserRepository
    {
        void SaveUser(UserModel user);
        UserModel GetUser(string login);
    }

    public class SimpleUserRepository : IUserRepository
    {
        private readonly Dictionary<string, UserModel> _users = new Dictionary<string, UserModel>();

        public UserModel GetUser(string login)
        {
            if (_users.TryGetValue(login, out UserModel user))
            {
                return user;
            }
            else return null;
        }

        public void SaveUser(UserModel user)
        {
            _users.Add(user.Login, user);
        }
    }
}
