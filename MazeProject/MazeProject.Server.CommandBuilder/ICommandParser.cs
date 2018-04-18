using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.Server.CommandBuilder.CommandAction;

namespace MazeProject.Server.CommandBuilder
{
    public interface ICommandParser
    {
        ActAbstract ParseCommand(byte[] bytes, System.Net.Sockets.Socket socket);
    }
}
