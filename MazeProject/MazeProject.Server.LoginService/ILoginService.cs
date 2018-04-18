using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.Server.LoginService
{
    public interface ILoginService
    {
        Guid Registration(String login, String password);
        Guid Login(String login, String password);
    }
}
