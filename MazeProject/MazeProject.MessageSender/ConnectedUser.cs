using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace MazeProject.MessageSender
{
    public class ConnectedUser
    {
        public Socket UserSocket { get; set; }
        public Guid UserID { get; set; }

        public ConnectedUser(Socket socket, Guid id)
        {
            UserSocket = socket;
            UserID = id;
        }
    }
}
