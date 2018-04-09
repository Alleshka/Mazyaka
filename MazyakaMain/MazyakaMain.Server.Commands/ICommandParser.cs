using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazyakaMain.Server.Commands
{
    public interface ICommandParser
    {
        AbstractCommandAction ParseCommand(byte[] data);
    }
}
