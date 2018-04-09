using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazyakaMain.MazeGeneral;
using MazyakaMain.MazeGeneral.Command;
namespace MazyakaMain.Server.Commands
{
    public abstract class AbstractCommandAction : ICommandAction
    {
        protected AbstractPackage request;
        protected AbstractPackage response;

        public AbstractCommandAction(AbstractPackage message)
        {
            response = message;
        }

        public abstract void Execute();
        public abstract AbstractPackage GetResponse();
        public abstract void SendResponse();
    }
}
