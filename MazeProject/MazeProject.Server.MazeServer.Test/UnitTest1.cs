using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net.Sockets;
using MazeProject.MazeGeneral.Command;
using MazeProject.Server.MazeServer;

namespace MazeProject.Server.MazeServer.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginTest()
        {
            MazeServer server = new MazeServer(1337);
            server.Start();

            Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.Connect("127.0.0.1", 1337);

            LoginRequest loginRequest = new LoginRequest("Alleshka", "123qwe");
            client.Send(LoginRequest.ToBytes(loginRequest));

            byte[] bytes = new byte[1024];
            client.Receive(bytes);
            LoginResponse loginResponse = LoginResponse.ToObject(bytes) as LoginResponse;
            Assert.AreEqual(typeof(Guid), loginResponse.UserID.GetType());
            server.Stop();
        }

        [TestMethod]
        public void LoginTest2()
        {
            MazeServer server = new MazeServer(1337);
            server.Start();

            Socket client1 = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client1.Connect("127.0.0.1", 1337);
            LoginRequest loginRequest1 = new LoginRequest("Alleshka", "123qwe");
            client1.Send(LoginRequest.ToBytes(loginRequest1));
            byte[] bytes1 = new byte[1024];
            client1.Receive(bytes1);
            LoginResponse login1 = LoginResponse.ToObject(bytes1) as LoginResponse;

            Socket client2 = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client2.Connect("127.0.0.1", 1337);
            LoginRequest loginRequest2 = new LoginRequest("Alleshka", "123qwe");
            client2.Send(LoginRequest.ToBytes(loginRequest2));
            byte[] bytes2 = new byte[1024];
            client2.Receive(bytes2);
            LoginResponse login2 = LoginResponse.ToObject(bytes2) as LoginResponse;

            UserCountRequest userCountRequest = new UserCountRequest(login1.UserID);
            client1.Send(UserCountRequest.ToBytes(userCountRequest));
            bytes1 = new byte[1024];
            client1.Receive(bytes1);
            UserCountResponse countResponse = UserCountResponse.ToObject(bytes1) as UserCountResponse;

            Assert.AreEqual(2, countResponse.UserCount);
            server.Stop();
        }
    }
}
