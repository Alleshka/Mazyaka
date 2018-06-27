using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MessageSender
{
    public class MessageSender
    {
        private List<ConnectedUser> userList;

        public MessageSender()
        {
            userList = new List<ConnectedUser>();
        }

        public void AddUser (Socket socket, Guid user)
        {
            userList.Add(new ConnectedUser(socket, user));
        }

        public void RemoveUser (Guid id)
        {
            userList.RemoveAll(x => x.UserID == id);
        }
        public void RemoveUser (Socket socket)
        {
            userList.RemoveAll(x => x.UserSocket == socket);
        }
    
        public bool ExistUser(Socket socket)
        {
            return userList.Any(x => x.UserSocket == socket);
        }
        public bool ExistUser(Guid id)
        {
            return userList.Any(x => x.UserID == id);
        }

        public void Cleare()
        {
            userList.Clear();
        }

        public Guid GetUserGuid(Socket socket)
        {
            return userList.Find(x => x.UserSocket == socket).UserID;
        }
        public Socket GetUserSocket(Guid userID)
        {
            return userList.Find(x => x.UserID == userID).UserSocket;
        }

        public void SendMessage(Guid userID, String message)
        {
            // TODO: Возможно пригодится отправлять пустые пакеты
            if((message!=null)&&(message!="")) GetUserSocket(userID).Send(Encoding.UTF8.GetBytes(message));
        }
        public void SendMessage(List<Guid> userList, String message)
        {
            foreach (Guid user in userList)
            {
                SendMessage(user, message);
            }
        }
        public void SendMessage(Tuple<List<Guid>, String> tuple)
        {
            SendMessage(tuple.Item1, tuple.Item2);
        }
    }
}
