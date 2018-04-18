using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    /// <summary>
    /// Запрос для входа
    /// </summary>
    [DataContract]
    public class LoginRequest : AbstractRequest
    {
        [DataMember]
        public String Login { get; set; }

        [DataMember]
        public String Password { get; set; }

        public LoginRequest(String login, String password) : base()
        {
            Login = login;
            Password = password;
        }
    }

    /// <summary>
    /// Ответ входа
    /// </summary>
    [DataContract]
    public class LoginResponse : AbstractResponse
    {
        [DataMember]
        public Guid UserID { get; set; }

        public LoginResponse(Guid id)
        {
            UserID = id;
        }
    }
}
