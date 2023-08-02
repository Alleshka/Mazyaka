using System;
using System.Collections.Generic;

namespace Maze.MazeStructure
{
    public class RecursiveMazeGenerator : IMazeGenerator
    {
        private Random Random = new Random();
        private int size = 10;

        public IMaze GenerateMaze(IMazeBuilder builder)
        {
            builder.BuildMaze();
            var maze = builder.GetMaze();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    builder.BuildRoom(i, j);
                }
            }

            int curLine = Random.Next(size);
            int curColumnt = Random.Next(size);

            var stackCell = new Stack<IMazeRoom>();
            var curCell = maze.GetRoomByCoordinates(curLine, curColumnt);
            curCell.IsVisited = true;


            while (HasNotVisitedCells(maze, size, size))
            {
                var newCell = GetNotVisitedNeighbor(maze, curCell);
                if (newCell != null)
                {
                    stackCell.Push(curCell);

                    builder.BuildPassage(curCell, newCell);
                    curCell = newCell; ;
                    curCell.IsVisited = true;
                }
                else
                {
                    if (stackCell.Count != 0)
                    {
                        curCell = stackCell.Pop();
                    }
                }
            }

            return maze;
        }

        private bool HasNotVisitedCells(IMaze maze, int lineCount, int columnCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cell = maze.GetRoomByCoordinates(i, j);
                    if (cell.IsVisited == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private IMazeRoom GetNotVisitedNeighbor(IMaze maze, IMazeRoom curCell)
        {
            var neighbords = new List<IMazeRoom>();

            // TODO: Fix copypaste

            // UP
            if (curCell.Line > 0)
            {
                var cell = maze.GetRoomByCoordinates(curCell.Line - 1, curCell.Column);
                if (!cell.IsVisited)
                {
                    neighbords.Add(cell);
                }
            }

            // Down
            if (curCell.Line < size - 1)
            {
                var cell = maze.GetRoomByCoordinates(curCell.Line + 1, curCell.Column);
                if (!cell.IsVisited)
                {
                    neighbords.Add(cell);
                }
            }

            // Left
            if (curCell.Column > 0)
            {
                var cell = maze.GetRoomByCoordinates(curCell.Line, curCell.Column - 1);
                if (!cell.IsVisited)
                {
                    neighbords.Add(cell);
                }
            }

            // Right
            if (curCell.Column < size - 1)
            {
                var cell = maze.GetRoomByCoordinates(curCell.Line, curCell.Column + 1);
                if (!cell.IsVisited)
                {
                    neighbords.Add(cell);
                }
            }

            if (neighbords.Count == 0) return null;
            else return neighbords[Random.Next(neighbords.Count)];
        }
    }
}
