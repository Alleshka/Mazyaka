using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.General.Package;

namespace MazeProject.CommandBuilder
{
    public interface ICommand
    {
        List<BaseResponse> Execute();
    }

    public abstract class BaseCommand : ICommand
    {
        protected BasePackage Request;
        
        public BaseCommand(BasePackage request)
        {
            this.Request = request;
        }

        public abstract List<BaseResponse> Execute();
    }
}
