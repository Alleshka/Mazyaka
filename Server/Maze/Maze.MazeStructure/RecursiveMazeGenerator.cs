﻿using Maze.Common;
using Maze.MazeStructure.Interfaces;

namespace Maze.MazeStructure
{
    public class RecursiveMazeGenerator : IMazeGenerator
    {
        private Random Random = new Random();

        private class CellsVisitStatus
        {
            private Dictionary<MazePoint, bool> _cellsVisitStatus;

            public CellsVisitStatus(int capacity)
            {
                _cellsVisitStatus = new Dictionary<MazePoint, bool>(capacity);
            }

            public void Add(int line, int col)
            {
                Add(new MazePoint(line, col));
            }

            public void Add(MazePoint point)
            {
                _cellsVisitStatus.Add(point, false);
            }

            public bool IsVisited(int line, int col)
            {
                return IsVisited(new MazePoint(line, col));
            }

            public bool IsVisited(MazePoint point)
            {
                return _cellsVisitStatus[point];
            }

            public void Visit(int line, int col)
            {
                Visit(new MazePoint(line, col));
            }

            public void Visit(MazePoint point)
            {
                _cellsVisitStatus[point] = true;
            }
        }

        public IMaze GenerateMaze(IMazeBuilder builder, int rowCount, int colCount)
        {
            var cellsVisitStatus = new CellsVisitStatus(rowCount * colCount);

            builder.BuildEmptyMaze(rowCount, colCount);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    builder.BuildRoom(i, j);
                    cellsVisitStatus.Add(i, j);
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (i > 0)
                    {
                        builder.BuildWall(i, j, i - 1, j);
                    }
                    else
                    {
                        builder.BuildNonDestroyableWall(i, j, MoveDirection.Up);
                    }

                    if (i < rowCount - 1)
                    {
                        builder.BuildWall(i, j, i + 1, j);
                    }
                    else
                    {
                        builder.BuildNonDestroyableWall(i, j, MoveDirection.Down);
                    }


                    if (j > 0)
                    {
                        builder.BuildWall(i, j, i, j - 1);
                    }
                    else
                    {
                        builder.BuildNonDestroyableWall(i, j, MoveDirection.Left);
                    }

                    if (j < colCount - 1)
                    {
                        builder.BuildWall(i, j, i, j + 1);
                    }
                    else
                    {
                        builder.BuildNonDestroyableWall(i, j, MoveDirection.Right);
                    }
                }
            }

            int curLine = Random.Next(rowCount);
            int curColumn = Random.Next(colCount);

            var stackCell = new Stack<MazePoint>();
            var curCell = new MazePoint(curLine, curColumn);

            while (HasNotVisitedCells(rowCount, colCount, cellsVisitStatus))
            {
                var newCell = GetNotVisitedNeighbor(curCell, rowCount, colCount, cellsVisitStatus);
                if (newCell != null)
                {
                    stackCell.Push(curCell);

                    builder.BuildPassage(curCell, newCell);
                    curCell = newCell;
                    cellsVisitStatus.Visit(curCell);
                }
                else
                {
                    if (stackCell.Count != 0)
                    {
                        curCell = stackCell.Pop();
                    }
                }
            }

            var directions = new List<MoveDirection>(4)
            {
                MoveDirection.Up,
                MoveDirection.Right,
                MoveDirection.Down,
                MoveDirection.Left
            };

            var exitSide = directions[Random.Next(4)];
            var exitNum = Random.Next(10);

            switch (exitSide)
            {
                case MoveDirection.Up:
                    {
                        builder.BuildExit(0, exitNum);
                        break;
                    }
                case MoveDirection.Down:
                    {
                        builder.BuildExit(9, exitNum);
                        break;

                    }
                case MoveDirection.Left:
                    {
                        builder.BuildExit(exitNum, 0);
                        break;
                    }
                case MoveDirection.Right:
                    {
                        builder.BuildExit(exitNum, 9);
                        break;
                    }
            }

            return builder.GetMaze();
        }

        private bool HasNotVisitedCells(int lineCount, int columnCount, CellsVisitStatus cellsVisitStatus)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cell = new MazePoint(i, j);
                    if (!cellsVisitStatus.IsVisited(cell))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private MazePoint GetNotVisitedNeighbor(MazePoint curCell, int lineCount, int colCount, CellsVisitStatus cellsVisitStatus)
        {
            var neighbords = new List<MazePoint>();

            // TODO: Fix copypaste

            // UP
            if (curCell.Row > 0)
            {
                var cell = new MazePoint(curCell.Row - 1, curCell.Column);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            // Down
            if (curCell.Row < lineCount - 1)
            {
                var cell = new MazePoint(curCell.Row + 1, curCell.Column);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            // Left
            if (curCell.Column > 0)
            {
                var cell = new MazePoint(curCell.Row, curCell.Column - 1);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            // Right
            if (curCell.Column < colCount - 1)
            {
                var cell = new MazePoint(curCell.Row, curCell.Column + 1);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            if (neighbords.Count == 0) return null;
            else return neighbords[Random.Next(neighbords.Count)];
        }
    }
}
