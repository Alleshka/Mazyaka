using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MazeProject.MazeGeneral.Command;
using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral.Serializier;


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
            MazeStruct str = new MazeStruct(new MazeGeneral.Maze.MazeGenerators.ReqursiveGenerator(null).GenerateMazeCells(10));

            CompressXmlSerializer xml = new CompressXmlSerializer();
            NewtosoftJsonSerialzer newtosoftJson = new NewtosoftJsonSerialzer();
            CompressNewtosoftSerializer compressNewtosoftSerializer = new CompressNewtosoftSerializer();

            string strXml = xml.ToStringFormat<MazeStruct>(str);
            string strJSON = newtosoftJson.ToStringFormat<MazeStruct>(str);
            string strCompressJSON = compressNewtosoftSerializer.ToStringFormat<MazeStruct>(str);

            if (strJSON.Length < strXml.Length) Assert.Fail();
            if (strCompressJSON.Length < strXml.Length) Assert.Fail();
        }
    }
}
