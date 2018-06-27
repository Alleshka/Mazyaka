using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General.Package
{
    public abstract class BasePackage
    {
        public String TypePackage { get; private set; }

        public BasePackage(String typePackage)
        {
            this.TypePackage = typePackage;
        }
    }

    public abstract class BaseRequest : BasePackage
    {
        public BaseRequest (String typeRequest) : base(typeRequest)
        {

        }
    }

    public abstract class BaseResponse : BasePackage
    {
        public BaseResponse (String typeResponse) : base(typeResponse)
        {

        }
    }
}
