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
        private List<Guid> Receives;

        public BaseResponse(String typeResponse) : base(typeResponse)
        {
            Receives = new List<Guid>();
        }

        public void AddReceive(params Guid[] users)
        {
            Receives.AddRange(users);
        }

        public List<Guid> GetReceives()
        {
            return new List<Guid>(Receives);
        }
    }
}
