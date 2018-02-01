
using System.Collections.Generic;

namespace Mazyaka.Model.StructLabirint
{
    /// <summary>
    /// Ячейка лабиринта    
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Адрес ячейки
        /// </summary>
        public Point Address { get; private set; }

        /// <summary>
        /// Объекты в ячейке
        /// </summary>
        public List<GameObject> Content { get; private set; }

        /// <summary>
        /// Ссылки на соседние ячейки. 
        /// Если стена - null
        /// </summary>
        public Cell UP { get; set; }
        public Cell LEFT { get; set; } 
        public Cell RIGHT { get; set; }
        public Cell DOWN { get; set; }

        public Cell(int line, int column)
        {
            Init(line, column);
        }
        public Cell(Point point)
        {
            Init(point.Line, point.Column);
        }

        private void Init(int line, int column)
        {
            Address = new Point(line, column);
            Content = new List<GameObject>();

            UP = null;
            LEFT = null;
            RIGHT = null;
            DOWN = null;
        }

        public void Add(GameObject obj)
        {
            Content.Add(obj);
        }
    }
}