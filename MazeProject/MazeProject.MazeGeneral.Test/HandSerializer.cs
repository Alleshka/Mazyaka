using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MazeProject.MazeGeneral.Command;
using MazeProject.MazeGeneral.Maze;
using Newtonsoft.Json;

namespace MazeProject.MazeGeneral.Test
{
    [TestClass]
    public class HandSerTest
    {
        [TestMethod]
        public void Hand()
        {
            var obj = new YourStep();
            Assert.AreEqual("[Type:YourStep]", obj.ToString());
        }

        [TestMethod]
        public void NewtosoftSer()
        {
            MazeStruct @str = new MazeStruct(new MazeGeneral.Maze.MazeGenerators.ReqursiveGenerator(null).GenerateMazeCells(10));
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(str);
            String xml = Serializer<MazeStruct>.ToXml(str);

            if (json.Length < xml.Length) Assert.Fail();   
        }
    }
}
