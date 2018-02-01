using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mazyaka.Model.StructLabirint;
using Mazyaka.Model.LabirintGenerator;
using Mazyaka.Model.LiveGameObjects;

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
            labirint.GenerateLabirint(new RecursiveGenerator());

            Assert.AreEqual(n, labirint.Size);
        }

        // TODO: ����� ������ ����, ��� ��� ��������� - ���������. �� ����, ��� ���������
        [TestMethod]
        public void MoveTest()
        {
            GameLabirint labirint = new GameLabirint(); // ������ ����

            // ������ ��������
            StructLabirint.StructLabirint @struct = new StructLabirint.StructLabirint(3);
            @struct.GenerateLabirint(new RecursiveGenerator());

            // ��������� � ����
            labirint.SetLabirintStruct(@struct);

            // ������ ��������
            Human human = new Human
            {
                Position = new Point(1, 1)
            };

            labirint.AddGameObject(human); // ��������� ��������


            labirint.MoveLiveObject(human.ID, MoveDirection.RIGHT);

            Assert.AreEqual(human.Position.Line, 2);
            Assert.AreEqual(human.Position.Column, 2);
        }
    }
}
