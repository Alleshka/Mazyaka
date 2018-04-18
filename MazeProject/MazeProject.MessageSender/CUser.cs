using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace MazeProject.Server.MessageSender
{
    public class CUser
    {
        public Guid UserID { get; set; }
        public Socket UserSocket { get; set; }

        public CUser(Guid id, Socket socket)
        {
            UserID = id;
            UserSocket = socket;
        }
    }
}
