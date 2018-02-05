using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mazyaka.Server.LoginService
{
    public class TestLoginService : ILoginService
    {
        public Guid Login(string login, string password)
        {
            return Guid.NewGuid();
        }
    }
}