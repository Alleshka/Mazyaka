using System;

namespace MazaProject.General.Package
{
    public abstract class BasePackage
    {
        public String Type { get; private set; }

        public BasePackage(String typePackage)
        {
            this.Type = typePackage;
        }
    }

    public abstract class BaseRequest : BasePackage
    {
        public BaseRequest(String typeRequest) : base (typeRequest)
        {

        }
    }

    public abstract class BaseResponse : BasePackage
    {
        public BaseResponse(String typeResponse) : base (typeResponse)
        {
        }
    }
}
