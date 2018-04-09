using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mazyaka.MazeClientLibrary;

namespace Mazyaka.Server.ServerWork.Test
{
    [TestClass]
    public class UnitTest1
    {
        private int port = 1337;
        private String ip = "127.0.0.1";

        [TestMethod]
        public void LoginTest()
        {
            MazeServer server = new MazeServer(new GameService.GameServiceFirst(), new LoginService.LoginServiceFirst(), port); // Create server
            server.Start();

            // Act
            MazeClient client = new MazeClient();
            client.Connect(ip, port);

            var temp = client.Login("Alleshka", "Alleshka13372");

            server.Stop();
        }

        [TestMethod]
        public void CreateGameTest()
        {
            MazeServer server = new MazeServer(new GameService.GameServiceFirst(), new LoginService.LoginServiceFirst(), port); // Create server
            server.Start();

            MazeClient client = new MazeClient();
            client.Connect(ip, port);

            var temp = client.Login("Alleshka", "Alleshka13372");
            var gameID = client.CreateGame(temp);

            server.Stop();
        }

        [TestMethod]
        public void JoinGameTest()
        {
            MazeServer server = new MazeServer(new GameService.GameServiceFirst(), new LoginService.LoginServiceFirst(), port); // Create server
            server.Start();

            MazeClient client = new MazeClient();
            client.Connect(ip, port);

            var user1 = client.Login("Alleshka", "Alleshka13372");
            var user2 = client.Login("Alleshka", "Alleshka13372");
            var gameID = client.CreateGame(user1);
            var temp = client.JoinGame(gameID, user2);

            server.Stop();
        }

        [TestMethod]
        public void TestGame()
        {
            MazeServer server = new MazeServer(new GameService.GameServiceFirst(), new LoginService.LoginServiceFirst(), port); // Create server
            server.Start();


            MazeClient client = new MazeClient();
            client.Connect(ip, port);

            var user1 = client.Login("Alleshka", "Alleshka13372");
            var user2 = client.Login("Alleshka", "Alleshka13372");

            var gameID = client.CreateGame(user1);
            var temp = client.JoinGame(gameID, user2);
        }
    }
}
