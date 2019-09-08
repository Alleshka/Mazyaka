using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.ServerLibrary
{
    interface IRegisterService
    {
        Guid Register(IUserData user);
        IUserData Login(string login, string password);
    }
}
