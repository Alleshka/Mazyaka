using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.LoginService
{
    public interface ILoginService
    {
        Guid Login(String login, String password);
    }
}
