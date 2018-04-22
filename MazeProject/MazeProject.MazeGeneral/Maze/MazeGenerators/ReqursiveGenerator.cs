using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze.MazeGenerators
{
    public class ReqursiveGenerator : IMazeGenerator
    {
        /// <summary>
        /// Посещена ли ячейка
        /// </summary>
        private bool[,] visited;
        private Cell[][] cells;

        private Random T;

        public ReqursiveGenerator(Random random)
        {
            if (random == null) T = new Random();
            else T = random;
        }

        public MazeStruct GenerateMazeStruct(int size)
        {
            return new MazeStruct(GenerateMazeCells(size));
        }

        public Cell[][] GenerateMazeCells(int size)
        {
            Init(size);
            Stack<Cell> StackCell = new Stack<Cell>();
            int startLine = T.Next(0, size);
            int startColumnt = T.Next(0, size);

            // Выбираем начальную ячейку и помечаем, что она посещена
            Cell curCell = cells[startLine][startColumnt];
            visited[startLine, startColumnt] = true;

            // Пока есть непосещённые ячейки
            while (IsNoVisitedExist())
            {
                // Если есть непосещённые соседи
                if (CheckExistNeighbord(curCell))
                {
                    StackCell.Push(curCell); // Проталкиваем в стек
                    curCell = GetRandNeighbord(curCell); // Выбираем случайного соседа и удаляем между ними стенки
                    visited[curCell.Address.Line, curCell.Address.Column] = true; // Отмечаем новую ячейку посещённой
                }
                else // Если нет
                {
                    // Если стек не пуст
                    if (StackCell.Count != 0)
                    {
                        curCell = StackCell.Pop();
                    }
                }
            }

            //MazeStruct maze = new MazeStruct(this.sizeLabirint);
            //maze.SetCells(Cells);

            return cells;
        }

        private void Init(int size)
        {
            visited = new bool[size, size];
            cells = new Cell[size][];

            for (int i = 0; i < size; i++)
            {
                cells[i] = new Cell[size];
                for (int j = 0; j < size; j++)
                {
                    cells[i][j] = new Cell(i, j);
                    visited[i, j] = false;
                }
            }
        }


        /// <summary>
        /// Проверяет, остались ли непосещённые ячейки
        /// </summary>
        /// <returns></returns>
        private bool IsNoVisitedExist()
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    if (visited[i, j] == false)
                        return true; // Возвращаем, что ещё есть
                }
            }

            return false;
        }


        /// <summary>
        /// Проверяет есть ли непосещённые соседи у текущей ячейке
        /// </summary>
        /// <param name="curCell"></param>
        /// <returns></returns>
        private bool CheckExistNeighbord(Cell curCell)
        {
            int curLine = curCell.Address.Line;
            int curColumn = curCell.Address.Column;

            if (curLine != 0 && !visited[curLine - 1, curColumn]) return true; // Проверяем верхнюю клетку
            if (curColumn != visited.GetLength(1) - 1 && !visited[curLine, curColumn + 1]) return true; // Проверяем правую клетку
            if (curLine != visited.GetLength(0) - 1 && !visited[curLine + 1, curColumn]) return true; // Проверяем нижнюю клетку
            if (curColumn != 0 && !visited[curLine, curColumn - 1]) return true; // Проверяем левую клетку

            return false; // Непосещённых соседей нет
        }

        /// <summary>
        /// Выбрать случайного соседа и удалить стены между этими ячейками
        /// </summary>
        /// <param name="curCell"></param>
        /// <returns></returns>
        private Cell GetRandNeighbord(Cell curCell)
        {
            List<MoveDirection> listDirection = new List<MoveDirection>();
            int curLine = curCell.Address.Line;
            int curColumn = curCell.Address.Column;

            // Добавляем возможные направления
            if (curLine != 0 && !visited[curLine - 1, curColumn]) listDirection.Add(MoveDirection.UP);
            if (curColumn != visited.GetLength(1) - 1 && !visited[curLine, curColumn + 1]) listDirection.Add(MoveDirection.RIGHT);
            if (curLine != visited.GetLength(0) - 1 && !visited[curLine + 1, curColumn]) listDirection.Add(MoveDirection.DOWN);
            if (curColumn != 0 && !visited[curLine, curColumn - 1]) listDirection.Add(MoveDirection.LEFT);

            MoveDirection direction = listDirection[T.Next(listDirection.Count)]; // Выбираем случайное направление
            Cell newCell = null;

            switch (direction)
            {
                // Выбираем верхнего соседа
                case MoveDirection.UP:
                    {
                        newCell = cells[curLine - 1][curColumn];

                        curCell.Up = newCell;
                        newCell.Down = curCell;

                        break;
                    }

                // Выбираем правого соседа
                case MoveDirection.RIGHT:
                    {
                        newCell = cells[curLine][curColumn + 1];

                        curCell.Right = newCell;
                        newCell.Left = curCell;

                        break;
                    }

                // Выбираем нижнего
                case MoveDirection.DOWN:
                    {
                        newCell = cells[curLine + 1][curColumn];

                        curCell.Down = newCell;
                        newCell.Up = curCell;

                        break;
                    }

                // Выбираем левого
                case MoveDirection.LEFT:
                    {
                        newCell = cells[curLine][curColumn - 1];

                        curCell.Left = newCell;
                        newCell.Right = curCell;

                        break;
                    }
            }

            return newCell;
        }
    }
}
