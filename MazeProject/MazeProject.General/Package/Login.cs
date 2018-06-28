using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MazeProject.General.Package
{
    public class LoginRequest : BaseRequest
    {
        public String Login { get; private set; }
        public String Passwod { get; private set; }

        public LoginRequest (String login, String password) : base("Login")
        {
            this.Login = login;
            this.Passwod = password;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class LoginResponse : BaseResponse
    {
        public String Login { get; private set; }
        public Guid ID { get; private set; }

        public LoginResponse(String login, Guid id) : base("Login")
        {
            this.Login = login;
            this.ID = id;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
