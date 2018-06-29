using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net.Sockets;
using System.Linq;
using MazeProject.Server;
using MazeProject.General.Package;
using Newtonsoft.Json.Linq;

namespace MazeProject.Server.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginTest()
        {
            // Asserts
            int port = 1337;
            String ip = "127.0.0.1";
            String usLogin = "Alleshka";


            MazeServer server = new MazeServer(port);
            server.Start();

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);


            LoginRequest login = new LoginRequest(usLogin, usLogin);
            socket.Send(System.Text.Encoding.UTF8.GetBytes(login.ToString()));

            byte[] buffer = new byte[2048];
            socket.Receive(buffer);

            String jsonString = System.Text.Encoding.UTF8.GetString(buffer.TakeWhile(x=>x!=0).ToArray());
            System.Diagnostics.Trace.WriteLine(jsonString);
            server.Stop();

            JObject jObject = JObject.Parse(jsonString);
            String type = (String)jObject["TypePackage"];
            String jlogin = (String)jObject["Login"];

            Assert.AreEqual(usLogin, jlogin);
            Assert.AreEqual(type, "Login");
        }
    }
}
