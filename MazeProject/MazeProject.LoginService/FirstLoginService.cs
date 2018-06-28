using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.LoginService
{
    public class FirstLoginService : ILoginService
    {
        public Guid Login(string login, string password)
        {
            return Guid.NewGuid();
        }
    }
}
