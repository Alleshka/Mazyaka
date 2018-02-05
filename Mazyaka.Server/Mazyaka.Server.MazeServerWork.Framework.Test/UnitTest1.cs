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


namespace Mazyaka.Server.MazeServerWork.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetID()
        {
            // Arrange
            MazeServer server = new MazeServer(new TestLoginService()); // Создаём сервер
            server.Start(); // Запускаем

            // Создаём клиента
            Mazyaka.CommonModel.MazeConnection.MazeConnection connection = new CommonModel.MazeConnection.MazeConnection();
            connection.Connect("127.0.0.1", 1337);
            Guid id = connection.Login("Alleshka", "Alleshka");

            server.Stop();

            Assert.AreEqual(id.GetType().ToString(), "System.Guid");
        }
    }
}
