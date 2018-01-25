using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mazyaka.Model.LabirintGenerator;

namespace Mazyaka.Model.StructLabirint
{
    /// <summary>
    /// Структура лабиринта
    /// </summary>
    public class StructLabirint
    {
        /// <summary>
        /// Ячейки
        /// </summary>
        private Cell[,] Cells { get; set; }
        public int Size { get; private set; }

        public StructLabirint(int sizeLabirint = 10)
        {
            Size = sizeLabirint; 
        }

        public void GenerateLabirint()
        {
            RecursiveGenerator generator = new RecursiveGenerator(Size);
            Cells = generator.Generate();
        }
    }
}