using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mazyaka.Server.LoginService
{
    public interface ILoginService
    {
        Guid Login(String login, String password); // Вход по логину и паролю
    }
}
