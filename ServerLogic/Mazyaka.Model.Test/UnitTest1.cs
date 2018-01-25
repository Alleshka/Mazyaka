using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mazyaka.Model.StructLabirint;

namespace Mazyaka.Model.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int n = 3;

            StructLabirint.StructLabirint labirint = new StructLabirint.StructLabirint(n);
            labirint.GenerateLabirint();

            Assert.AreEqual(n, labirint.Size);
        }
    }
}
