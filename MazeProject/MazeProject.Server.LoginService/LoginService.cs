using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.Server.LoginService
{
    public class LoginService : ILoginService
    {
        public Guid Login(string login, string password)
        {
            return Guid.NewGuid();
        }

        public Guid Registration(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
