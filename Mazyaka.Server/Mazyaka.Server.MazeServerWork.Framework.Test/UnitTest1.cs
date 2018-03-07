using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Mazyaka;
using Mazyaka.CommonModel.MazeConnection;
using Mazyaka.Server.LoginService;
using Mazyaka.Server.GameService;


namespace Mazyaka.Server.MazeServerWork.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPlayGame()
        {
            // Arrange

            MazeServer server = new MazeServer(new TestLoginService(), new TestGameService(), 1337); // Create server
            server.Start(); // Start server

            // Act
            MazeConnection client1 = new MazeConnection(); // Create clients
            MazeConnection client2 = new MazeConnection();


            client1.Connect("127.0.0.1", 1337); // Connect to Server
            client2.Connect("127.0.0.1", 1337);


            Guid idClient1 = client1.Login("testUser1", "testpassword1"); // get users id
            Guid idClient2 = client2.Login("testUser2", "testpassword2");


            Guid roomId = client1.CreateGame(idClient1); // Create game
            client2.JoinGame(idClient2, roomId); // Join game

            client1.SendMaze(roomId, idClient1, null);
            client1.SendMaze(roomId, idClient2, null);

            
        }
    }
}
