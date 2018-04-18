using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeProject.Server.GameService.Test
{
    [TestClass]
    public class GameServiceTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Guid player1 = Guid.NewGuid();
            Guid player2 = Guid.NewGuid();

            IGameService gameService = new GameServiceFirst();

            // Act
            gameService.CreateGame(player1);
            int countGame1 = gameService.GamesList().Count;
            gameService.CreateGame(player2);
            int countGame2 = gameService.GamesList().Count;

            // Asserts
            Assert.AreEqual(1, countGame1);
            Assert.AreEqual(2, countGame2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            Guid player1 = Guid.NewGuid();
            Guid player2 = Guid.NewGuid();
            Guid player3 = Guid.NewGuid();
            Guid player4 = Guid.NewGuid();

            IGameService gameService = new GameServiceFirst();

            // Act
            Guid gameId1 = gameService.CreateGame(player1);
            int countGame1 = gameService.GamesList().Count;
            Guid gameId2 = gameService.CreateGame(player2);
            int countGame2 = gameService.GamesList().Count;
            gameService.JoinGame(player3, gameId1);
            int countGame3 = gameService.GamesList().Count;
            gameService.JoinGame(player4, gameId2);
            int countGame4 = gameService.GamesList().Count;

            // Asserts
            Assert.AreEqual(1, countGame1);
            Assert.AreEqual(2, countGame2);
            Assert.AreEqual(1, countGame3);
            Assert.AreEqual(0, countGame4);
        }

        [TestMethod]
        public void StepTest()
        {
            // Arrange
            Guid player1 = Guid.NewGuid();
            Guid player2 = Guid.NewGuid();

            IGameService gameService = new GameServiceFirst();

            // Act
            Guid gameID = gameService.CreateGame(player1);
            gameService.JoinGame(player2, gameID);

            gameService.AddMaze(gameID, player1);
            gameService.AddMaze(gameID, player2);

            gameService.AddLive(gameID, player1);
            gameService.AddLive(gameID, player2);

            bool step1_1 = gameService.MoveObject(gameID, player1, MazeGeneral.MoveDirection.UP);
            bool step2_1 = gameService.MoveObject(gameID, player2, MazeGeneral.MoveDirection.UP);

            bool step1_2 = gameService.MoveObject(gameID, player1, MazeGeneral.MoveDirection.RIGHT);
            bool step2_2 = gameService.MoveObject(gameID, player2, MazeGeneral.MoveDirection.RIGHT);

            bool step1_3 = gameService.MoveObject(gameID, player1, MazeGeneral.MoveDirection.LEFT);
            bool step2_3 = gameService.MoveObject(gameID, player2, MazeGeneral.MoveDirection.LEFT);

            bool step1_4 = gameService.MoveObject(gameID, player1, MazeGeneral.MoveDirection.DOWN);
            bool step2_4 = gameService.MoveObject(gameID, player2, MazeGeneral.MoveDirection.DOWN);

            bool step1 = step1_1 || step1_2 || step1_3 || step1_4;
            bool step2 = step2_1 || step2_2 || step2_3 || step2_4;

            Assert.AreEqual(true, step1);
            Assert.AreEqual(true, step2);
        }
    }
}
