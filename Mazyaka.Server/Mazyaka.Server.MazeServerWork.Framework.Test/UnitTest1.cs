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
using Mazyaka.Server.LoginService;
using Mazyaka.Server.GameService;


namespace Mazyaka.Server.MazeServerWork.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetID()
        {
            // Arrange
            //MazeServer server = new MazeServer(new TestLoginService(), new TestGameService()); // Создаём сервер
            //server.Start(); // Запускаем

            // Создаём клиента
            Mazyaka.CommonModel.MazeConnection.MazeConnection connection = new CommonModel.MazeConnection.MazeConnection();
            connection.Connect("127.0.0.1", 1337);
            Guid id = connection.Login("Alleshka", "Alleshka");

            //server.Stop();

            Assert.AreEqual(id.GetType().ToString(), "System.Guid");
        }

        [TestMethod]
        public void CreateAndJoinGame()
        {
            // Arrange
            //MazeServer server = new MazeServer(new TestLoginService(), new TestGameService()); // Создаём сервер
            //server.Start(); // Запускаем

            // Создаём клиента
            Mazyaka.CommonModel.MazeConnection.MazeConnection connection = new CommonModel.MazeConnection.MazeConnection();
            connection.Connect("127.0.0.1", 1337);
            Guid id1 = connection.Login("Alleshka", "Alleshka");
            Guid gameID1 = connection.CreateGame(id1);

            //Thread.Sleep(1000);

            Mazyaka.CommonModel.MazeConnection.MazeConnection connection2 = new CommonModel.MazeConnection.MazeConnection();
            connection2.Connect("127.0.0.1", 1337);
            Guid id2 = connection2.Login("Alleshka2", "Alleshka");
            Guid gameID2 = connection2.JoinGame(gameID1, id2);

            //server.Stop();

            Assert.AreEqual(gameID1, gameID2);
        }

        [TestMethod]
        public void TestPlayGame()
        {
            MazeServer server = new MazeServer(new TestLoginService(), new TestGameService()); // Создаём сервер
            server.Start(); // Запускаем

            // Создаём игру с первого клиента
            Mazyaka.CommonModel.MazeConnection.MazeConnection connection = new CommonModel.MazeConnection.MazeConnection();
            connection.Connect("127.0.0.1", 1337);
            Guid id1 = connection.Login("Alleshka", "Alleshka");
            Guid gameID1 = connection.CreateGame(id1);

            // Создаём игру со второго клиента
            Mazyaka.CommonModel.MazeConnection.MazeConnection connection2 = new CommonModel.MazeConnection.MazeConnection();
            connection2.Connect("127.0.0.1", 1337);
            Guid id2 = connection2.Login("Alleshka2", "Alleshka");
            Guid gameID2 = connection2.JoinGame(gameID1, id2);


        }
    }
}
