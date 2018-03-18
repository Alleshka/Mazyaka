using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazyaka.Server.LoginService
{
    public class LoginServiceFirst : ILoginService
    {
        public Guid Login(string login, string password)
        {
            return Guid.NewGuid();
        }
    }
}
