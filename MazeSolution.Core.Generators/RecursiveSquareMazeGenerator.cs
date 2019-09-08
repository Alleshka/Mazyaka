using System;
using System.Collections.Generic;
using System.Text;
using MazeSolution.Core.MazeStructrure;
using MazeSolution.Core.MazeStructrure.Cells;

namespace MazeSolution.Core.Generators
{
    public class RecursiveSquareMazeGenerator : IMazeGenerator
    {
        private Random T = new Random();

        public IMazeStructure GenerateMaze(int lineCount, int columnCount)
        {
            var result = new SquareMaze<SquareCell>(lineCount, columnCount);

            InitWallRelations(result, lineCount, columnCount);
            Generate(result, lineCount, columnCount);

            result.ColumnCount = columnCount;
            result.LineCount = lineCount;

            return result;
        }

        private void InitWallRelations(IMazeStructure result, int lineCount, int columnCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cell = result[i, j];

                    if (i == 0) cell.AddRelation(Direction.Up, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Up, RelationManager.GetRelationType<WallRelation>(), result[i - 1, j]);

                    if (j == 0) cell.AddRelation(Direction.Left, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Left, RelationManager.GetRelationType<WallRelation>(), result[i, j - 1]);

                    if (i == lineCount - 1) cell.AddRelation(Direction.Down, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Down, RelationManager.GetRelationType<WallRelation>(), result[i + 1, j]);

                    if (j == columnCount - 1) cell.AddRelation(Direction.Right, RelationManager.GetRelationType<MazeWallRelation>(), null);
                    else cell.AddRelation(Direction.Right, RelationManager.GetRelationType<WallRelation>(), result[i, j+1]);
                }
            }
        }

        private void Generate(IMazeStructure result, int lineCount, int columnCount)
        {
            int curLine = T.Next(lineCount);
            int curColumn = T.Next(columnCount);

            var stackCells = new Stack<ICell>();
            var curCell = result[curLine, curColumn];
            curCell.Visited = true;

            while (HasNotVisitedCells(result, lineCount, columnCount)) // Пока есть непосещённые ячейки
            {
                var newCell = GetNotVisitedNeighbords(curCell);
                if (newCell != null) // Имеет непосещённых соседей
                {
                    stackCells.Push(curCell);

                    // Убираем между ячейками стены 
                    var rel = CellSupport.GetCellsRelation(curCell, newCell);
                    rel.RelationType = RelationManager.GetRelationType<Passage>();

                    rel = CellSupport.GetCellsRelation(newCell, curCell);
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
        private bool HasNotVisitedCells(IMazeStructure result, int lineCount, int columnCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (result[i, j].Visited == false) return true;
                }
            }

            return false;
        }

        private ICell GetNotVisitedNeighbords(ICell curCell)
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
