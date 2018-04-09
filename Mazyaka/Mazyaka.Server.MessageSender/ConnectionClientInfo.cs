using System;
using System.Net.Sockets;

namespace Mazyaka.Server.MessageSender
{
    public class ConnectionClientInfo
    {
        public Guid UserID { get; set; } // ID пользователя в базе
        public Socket UserSocket { get; set; } // Сокет с соединением данного пользователя

        public ConnectionClientInfo(Guid userID, Socket userSocket)
        {
            UserID = userID;
            UserSocket = userSocket;
        }
    }
}
