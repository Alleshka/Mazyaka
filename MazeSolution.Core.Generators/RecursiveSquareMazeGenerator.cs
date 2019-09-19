using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.Core.MazeStructrure;
using MazeSolution.Core.MazeStructrure.Cells;

namespace MazeSolution.Core.Generators
{
    /// <summary>
    /// Рекурсивный генератор лабиринта
    /// </summary>
    public class RecursiveSquareMazeGenerator : IMazeGenerator
    {
        private Random T = new Random();

        /// <summary>
        /// Сгенерировать лабиринт
        /// </summary>
        /// <param name="lineCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        /// <returns></returns>
        public IMazeStructure GenerateMaze(int lineCount, int columnCount)
        {
            var result = new SquareMaze<SquareCell>(lineCount, columnCount);

            InitWallRelations(result, lineCount, columnCount);
            Generate(result, lineCount, columnCount);

            result.ColumnCount = columnCount;
            result.LineCount = lineCount;

            return result;
        }

        /// <summary>
        /// Установить соотношения ячеек к стенам лабиринта
        /// </summary>
        /// <param name="structure">Структура лабиринта</param>
        /// <param name="lineCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        private void InitWallRelations(IMazeStructure structure, int lineCount, int columnCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cell = structure[i, j];

                    if (i == 0) cell.AddRelation(Direction.Up, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Up, RelationManager.GetRelationType<WallRelation>(), structure[i - 1, j]);

                    if (j == 0) cell.AddRelation(Direction.Left, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Left, RelationManager.GetRelationType<WallRelation>(), structure[i, j - 1]);

                    if (i == lineCount - 1) cell.AddRelation(Direction.Down, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Down, RelationManager.GetRelationType<WallRelation>(), structure[i + 1, j]);

                    if (j == columnCount - 1) cell.AddRelation(Direction.Right, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Right, RelationManager.GetRelationType<WallRelation>(), structure[i, j+1]);
                }
            }
        }

        /// <summary>
        /// Сгенерировать структуру лабиринта
        /// </summary>
        /// <param name="structure">Структура лабиринта</param>
        /// <param name="lineCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        private void Generate(IMazeStructure structure, int lineCount, int columnCount)
        {
            int curLine = T.Next(lineCount);
            int curColumn = T.Next(columnCount);

            var stackCells = new Stack<ICell>();
            var curCell = structure[curLine, curColumn];
            curCell.Visited = true;

            while (HasNotVisitedCells(structure, lineCount, columnCount)) // Пока есть непосещённые ячейки
            {
                var newCell = GetNotVisitedNeighbors(curCell);
                if (newCell != null) // Имеет непосещённых соседей
                {
                    stackCells.Push(curCell);

                    // Убираем между ячейками стены 
                    var rel = curCell.GetCellsRelation(newCell);
                    rel.RelationType = RelationManager.GetRelationType<Passage>();

                    rel = newCell.GetCellsRelation(curCell);
                    rel.RelationType = RelationManager.GetRelationType<Passage>();

                    curCell = newCell;
                    curCell.Visited = true;
                }
                else
                {
                    if (stackCells.Count != 0)
                    {
                        curCell = stackCells.Pop();
                    }
                }
            }
        }

        // TODO: Сделать счётчик непосещённых
        /// <summary>
        /// Проверяет есть ли непосещенные ячейки
        /// </summary>
        /// <param name="structure">Структура лабиринта</param>
        /// <param name="lineCount">Число строк</param>
        /// <param name="columnCount">Число столбцов</param>
        /// <returns>True если есть непосещенне ячейки, иначе false</returns>
        private bool HasNotVisitedCells(IMazeStructure structure, int lineCount, int columnCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (structure[i, j].Visited == false) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получает одну случайную ячейку из непосещенных соседей
        /// </summary>
        /// <param name="curCell">Ячейка, соседи которой будут просмотрены</param>
        /// <returns>Следующую непосещенную ячейку из списка соседей текущей</returns>
        private ICell GetNotVisitedNeighbors(ICell curCell)
        {
            var neighbords = new List<ICell>();
            foreach (var relation in curCell.AllRelations)
            {
                var nextCell = curCell.GetRelation(relation.Key).GetNextCell;
                if (nextCell != null && nextCell.Visited == false) neighbords.Add(nextCell);
            }

            if (neighbords.Count == 0) return null;
            else return neighbords[T.Next(neighbords.Count)];
        }
    }
}
