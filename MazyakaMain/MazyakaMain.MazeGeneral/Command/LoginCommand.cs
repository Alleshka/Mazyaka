using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazyakaMain.MazeGeneral
{
    public class LoginRequest : AbstractPackage
    {
        public String Login { get; set; }
        public String Password { get; set; }

        public LoginRequest(String log, String pass) : base()
        {
            Login = log;
            Password = pass;
        }
    }

    public class LoginResponse : AbstractPackage
    {
        public Guid UserID { get; set; }

        public LoginResponse(Guid id) : base()
        {
            UserID = id;
        }
    }
}
