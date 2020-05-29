using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze.Server.UdpServer
{
    /// <summary>
    /// Базовый интерфейс пакета для передачи через TCP
    /// </summary>
    public interface IMazePackage
    {
        string TypeName { get; }
    }

    public abstract class BaseMazePackage : IMazePackage
    {
        public BaseMazePackage()
        {

        }

        public string TypeName => (this.GetType().ToString()).Split(".").Last();


        protected abstract void InitProps(BaseMazePackage obj);

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class LoginMazePackage : BaseMazePackage
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginMazePackage() : base()
        {

        }

        public LoginMazePackage(string login, string password) : base()
        {
            this.Login = login;
            this.Password = password;
        }

        protected override void InitProps(BaseMazePackage jObject)
        {
            var curObject = jObject as LoginMazePackage;
            this.Login = curObject.Login;
            this.Password = curObject.Password;
        }
    }
}
