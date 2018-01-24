using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mazyaka.Model.StructLabirint;

namespace Mazyaka.Model.LabirintGenerator
{
    public class RecursiveGenerator : ILabirintGenerator
    {
        /// <summary>
        /// Посещена ли ячейка
        /// </summary>
        private bool[,] visited;
        private Cell[,] Cells;

        private Random T;
        private int sizeLabirint;

        public RecursiveGenerator(int size)
        {
            visited = new bool[size, size];
            Cells = new Cell[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                    visited[i, j] = false;
                }
            }

            T = new Random();
            sizeLabirint = size;
        }

        public Cell[,] Generate()
        {
            int startLine = T.Next(0, sizeLabirint);
            int startColumnt = T.Next(0, sizeLabirint);

            // Выбираем начальную ячейку и помечаем, что она посещена
            Cell curCell = Cells[startLine, startColumnt];
            visited[startLine, startColumnt] = true;

            // Пока есть непосещённые ячейки
            while (IsNoVisitedExist())
            {


            }

            throw new NotImplementedException("Generate");
        }

        /// <summary>
        /// Проверяет, остались ли непосещённые ячейки
        /// </summary>
        /// <returns></returns>
        private bool IsNoVisitedExist()
        {
            throw new NotImplementedException("IsNoVisitedExist");
        }
    }
}