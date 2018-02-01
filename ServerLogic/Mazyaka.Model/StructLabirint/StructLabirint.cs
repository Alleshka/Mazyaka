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

        public void GenerateLabirint(ILabirintGenerator generator)
        {
            Cells = generator.Generate(Size); // Генерим лабиринт
        }

        public Cell this [int line, int column] => Cells[line, column];
        public Cell this [Point position] => Cells[position.Line, position.Column];
    }
}