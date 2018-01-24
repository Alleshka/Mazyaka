using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        private int size;

        public StructLabirint(int sizeLabirint = 10)
        {
            size = sizeLabirint; 
        }
    }
}