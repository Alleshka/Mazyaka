using System;
using System.Collections.Generic;
using System.Linq;
using Mazyaka.MazeGeneral;

namespace Mazyaka.Server.MessageSender
{
    public class SocketSender : AbstractSender
    {
        protected List<ConnectionClientInfo> clientList; // Список активных клиентов

        public SocketSender() : base()
        {
            clientList = new List<ConnectionClientInfo>();
        }

        public void AddClient(ConnectionClientInfo client)
        {
            clientList.Add(client);
        }
        public void RemoveClient(ConnectionClientInfo client)
        {
            clientList.Remove(client);
        }

        public override void Send(Guid userID, PackCommand pack)
        {
            var user = clientList.Where(x => x.UserID == userID).First(); // Находим активного клиента с этим ID
            user.UserSocket.Send(PackCommand.ToBytes(pack)); // Отправляем пользователю пакет
        }
        public override void Send(List<Guid> usersID, PackCommand pack)
        {
            foreach (var user in usersID)
            {
                Send(user, pack);
            }
        }
    }
}
