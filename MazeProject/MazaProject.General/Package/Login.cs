using System;
using System.Collections.Generic;
using System.Text;

namespace MazaProject.General.Package
{
    public class LoginRequest : BaseRequest
    {
        public String Login { get; private set; }
        public String Password { get; private set; }

        public LoginRequest(String login, String passwrod) : base ("Login")
        {
            this.Login = login;
            this.Password = passwrod;
        }
    }

    public class LoginResponse : BaseResponse
    {
        public String Login { get; set; }
        public Guid ID { get; set; }

        public LoginResponse(String login, Guid id) : base ("Login")
        {
            this.Login = login;
            this.ID = id;

        }
    }
}
