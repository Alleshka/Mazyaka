using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazyakaMain.MazeGeneral;

namespace MazyakaMain.Server.Commands
{
    public interface ICommandAction
    {
        void Execute();
        AbstractPackage GetResponse();
        void SendResponse();
    }
}
