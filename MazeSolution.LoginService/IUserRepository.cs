using MazeSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.LoginService
{
    /// <summary>
    /// Интерфейс репозитория пользователей
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Сохранить пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        void SaveUser(UserModel user);

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Пользователя</returns>
        UserModel GetUser(string login);
    }

    public class SimpleUserRepository : IUserRepository
    {
        private readonly Dictionary<string, UserModel> _users = new Dictionary<string, UserModel>();


        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Пользователя</returns>
        public UserModel GetUser(string login)
        {
            if (_users.TryGetValue(login, out UserModel user))
            {
                return user;
            }
            else return null;
        }

        /// <summary>
        /// Сохранить пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public void SaveUser(UserModel user)
        {
            _users.Add(user.Login, user);
        }
    }
}
