using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral.Maze.MazeGenerators;

namespace MazeProject.MazeGeneral.Test
{
    [TestClass]
    public class MazeGeneratorTest
    {
        [TestMethod]
        public void GenerateTest1()
        {
            // Arrange
            IMazeGenerator mazeGenerator = new ReqursiveGenerator();
            int mazeSize = 5;

            // Act
            MazeStruct mazeStruct = mazeGenerator.GenerateMazeStruct(mazeSize);

            //using (System.IO.StreamWriter stream = new System.IO.StreamWriter(Guid.NewGuid().ToString()))
            //{
            //    stream.Write(Serializer<MazeStruct>.ToXml(mazeStruct));
            //}

            // Asserts
            Assert.AreEqual(mazeSize, mazeStruct.Size);
        }
    }
}
