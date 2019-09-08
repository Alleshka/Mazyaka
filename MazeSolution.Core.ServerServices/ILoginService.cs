using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.ServerServices
{
    public interface ILoginService
    {
        IUserData Register(IUserData user);
        Guid Login(string login, string password);
    }
}
