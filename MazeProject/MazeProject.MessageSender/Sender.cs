using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Command;
using MazeProject.MazeGeneral;
using System.Net.Sockets;
using MazeProject.Server.Logger;

namespace MazeProject.Server.MessageSender
{
    public class Sender
    {
        public Sender()
        {
            userList = new List<CUser>();
        }

        private List<CUser> userList;
        public void AddUser(Guid id, System.Net.Sockets.Socket socket) => userList.Add(new CUser(id, socket));
        public void RemoveUser(Guid id) => userList.RemoveAll(x => x.UserID == id);
        public void RemoveUser(Socket socket) => userList.RemoveAll(x => x.UserSocket == socket);
        public void SendMessage(Guid userID, AbstractMessage message)
        {
            System.Diagnostics.Trace.WriteLine($"               Отправка пакета {message.ToString().Split('.').Last()} пользователю {userID}");
            userList.Where(x => x.UserID == userID).First().UserSocket.Send(AbstractMessage.ToBytes(message));
        }
        public void SendMessage(List<Guid> usersID, AbstractMessage message)
        {
            foreach (var user in usersID)
            {
                SendMessage(user, message);
            }
        }
        public bool ExistUser(System.Net.Sockets.Socket socket) => userList.Any(x => x.UserSocket == socket);
        public int GetUserCount() => userList.Count;
        public void Clear() => userList.Clear();
        public Guid GetUserID(Socket socket) => userList.Where(x => x.UserSocket == socket).First().UserID;
    }
}
