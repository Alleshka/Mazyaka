using MazeSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.LoginService
{
    /// <summary>
    /// Хранит пользователей с активными сессиями
    /// Генерит секретный ключ
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Сессию пользователя</returns>
        UserServiceModel AddUser(UserModel user);

        /// <summary>
        /// Проверка токена доступа
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>True если токен валидный, иначе false</returns>
        bool CheckUserSecurityKey(UserServiceModel user);
    }

    public class SimpleUserService : IUserService
    {
        private readonly Dictionary<Guid, UserServiceModel> _users = new Dictionary<Guid, UserServiceModel>();

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Сессию пользователя</returns>
        public UserServiceModel AddUser(UserModel user)
        {
            if (_users.TryGetValue(user.ObjectID, out UserServiceModel securityUser))
            {
                return securityUser;
            }
            else
            {
                return new UserServiceModel()
                {
                    ObjectID = user.ObjectID,
                    Login = user.Login
                };
            }
        }

        /// <summary>
        /// Проверка токена доступа
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>True если токен валидный, иначе false</returns>
        public bool CheckUserSecurityKey(UserServiceModel user)
        {
            if (_users.TryGetValue(user.ObjectID, out UserServiceModel secUser))
            {
                if (secUser.SecurityToken.Equals(user.SecurityToken))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
