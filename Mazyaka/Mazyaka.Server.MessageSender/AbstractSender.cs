using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mazyaka.MazeGeneral;

namespace Mazyaka.Server.MessageSender
{
    public abstract class AbstractSender
    {
        public AbstractSender()
        {

        }

        /// <summary>
        /// Отправить сообщение клиенту
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <param name="pack">Пакет данных</param>
        public abstract void Send(Guid userID, PackCommand pack);
        /// <summary>
        /// Отправить сообщение нескольким пользователям
        /// </summary>
        /// <param name="usersID">Список пользователей</param>
        /// <param name="pack">Пакет данных</param>
        public abstract void Send(List<Guid> usersID, PackCommand pack);
    }
}
