using Maze.Common;
using Maze.MazeStructure.Metadata;
using Maze.MazeStructure.MazeSites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure.MazeGenerators
{
    public class GridMazeGenerator : IMazeGenerator
    {
        private readonly int _rowCount;
        private readonly int _colCount;
        private Random Random = new Random();

        private class CellVisitedStatus
        {
            private Dictionary<(int, int), bool> _cellsVisitStatus;
            private int _notVisitedCount = 0;

            public CellVisitedStatus(int capacity)
            {
                _cellsVisitStatus = new Dictionary<(int, int), bool>(capacity);
            }

            public void Add(int row, int col)
            {
                Add((row, col));
            }

            public void Add((int row, int col) point)
            {
                _cellsVisitStatus.Add(point, false);
                _notVisitedCount++;
            }

            public bool IsVisited(int row, int col)
            {
                return IsVisited((row, col));
            }

            public bool IsVisited((int row, int col) point)
            {
                return _cellsVisitStatus[point];
            }

            public void Visit(int line, int col)
            {
                Visit((line, col));
            }

            public void Visit((int row, int col) point)
            {
                _cellsVisitStatus[point] = true;
                _notVisitedCount--;
            }

            public bool HasNotVisitedCells => _notVisitedCount > 0;
        }

        public GridMazeGenerator(int rowCount, int colCount)
        {
            _rowCount = rowCount;
            _colCount = colCount;
        }

        public IMazeInfo Generate()
        {
            IMazeBuilder builder = new SimpleMazeBuilder();
            IMazeRoom[,] rooms = new IMazeRoom[_rowCount, _colCount];
            CellVisitedStatus cellVisitedStatus = new CellVisitedStatus(_rowCount * _colCount);
            builder.BuildEmptyMaze();

            int count = 0;
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _colCount; j++)
                {
                    IMazeRoom room = new BaseMazeRoom(count);
                    cellVisitedStatus.Add(i, j);

                    rooms[i, j] = room;
                    builder.BuildRoom(room);
                    count++;
                }
            }

            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _colCount; j++)
                {
                    if (i == 0)
                    {
                        builder.BuildBoundary(rooms[i, j], MoveDirection.Up);
                    }
                    else
                    {
                        builder.BuildWall(rooms[i, j], MoveDirection.Up, rooms[i - 1, j]);
                    }

                    if (j == 0)
                    {
                        builder.BuildBoundary(rooms[i, j], MoveDirection.Left);
                    }
                    else
                    {
                        builder.BuildWall(rooms[i, j], MoveDirection.Left, rooms[i, j - 1]);
                    }

                    if (i == _rowCount - 1)
                    {
                        builder.BuildBoundary(rooms[i, j], MoveDirection.Down);
                    }
                    else
                    {
                        builder.BuildWall(rooms[i, j], MoveDirection.Down, rooms[i + 1, j]);
                    }

                    if (j == _colCount - 1)
                    {
                        builder.BuildBoundary(rooms[i, j], MoveDirection.Right);
                    }
                    else
                    {
                        builder.BuildWall(rooms[i, j], MoveDirection.Right, rooms[i, j + 1]);
                    }
                }
            }

            int curRow = Random.Next(_rowCount);
            int curCol = Random.Next(_colCount);

            var stackCell = new Stack<(int, int)>();
            var curCell = (curRow, curCol);

            while (cellVisitedStatus.HasNotVisitedCells)
            {
                var newCell = GetNotVisitedNeighbor(curCell, _rowCount, _colCount, cellVisitedStatus);
                if (newCell != null)
                {
                    stackCell.Push(curCell);

                    builder.BuildPassage(rooms[curCell.curRow, curCell.curCol], CommonWall(curCell, newCell.Value), rooms[newCell.Value.row, newCell.Value.col]);
                    curCell = newCell.Value;
                    cellVisitedStatus.Visit(curCell);
                }
                else
                {
                    if (stackCell.Count != 0)
                    {
                        curCell = stackCell.Pop();
                    }
                }
            }

            int exitNum = Random.Next(_rowCount);
            builder.BuildExit(rooms[exitNum, _colCount - 1], MoveDirection.Right);
            builder.BuildExit(rooms[_rowCount - 1, exitNum], MoveDirection.Down);
            builder.BuildExit(rooms[0, exitNum], MoveDirection.Up);
            builder.BuildExit(rooms[exitNum, 0], MoveDirection.Left);

            var result = builder.Build();
            return result;
        }

        private (int row, int col)? GetNotVisitedNeighbor((int row, int col) curCell, int lineCount, int colCount, CellVisitedStatus cellsVisitStatus)
        {
            var neighbords = new List<(int, int)>();

            // UP
            if (curCell.row > 0)
            {
                var cell = (curCell.row - 1, curCell.col);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            // Down
            if (curCell.row < lineCount - 1)
            {
                var cell = (curCell.row + 1, curCell.col);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            // Left
            if (curCell.col > 0)
            {
                var cell = (curCell.row, curCell.col - 1);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            // Right
            if (curCell.col < colCount - 1)
            {
                var cell = (curCell.row, curCell.col + 1);
                if (!cellsVisitStatus.IsVisited(cell))
                {
                    neighbords.Add(cell);
                }
            }

            if (neighbords.Count == 0) return null;
            else return neighbords[Random.Next(neighbords.Count)];
        }

        protected MoveDirection CommonWall((int row, int col) room1, (int row, int col) room2)
        {
            MoveDirection moveDirection = MoveDirection.None;

            if (room1.row == room2.row)
            {
                if (room1.col < room2.col)
                {
                    moveDirection = MoveDirection.Right;
                }
                else if (room1.col > room2.col)
                {
                    moveDirection = MoveDirection.Left;
                }
            }
            else if (room1.col == room2.col)
            {
                if (room1.row < room2.row)
                {
                    moveDirection = MoveDirection.Down;
                }
                else if (room1.row > room2.row)
                {
                    moveDirection = MoveDirection.Up;
                }
            }

            return moveDirection;
        }
    }
}
