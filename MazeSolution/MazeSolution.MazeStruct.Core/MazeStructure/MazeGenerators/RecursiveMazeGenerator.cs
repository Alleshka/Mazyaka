using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators
{
    /// <summary>
    /// TODO: потребуется переписать
    /// </summary>
    public class RecursiveMazeGenerator : IMazeGenerator
    {
        private Cell[][] _cells;

        private int _size;

        private Random T = new Random();

        public Cell[][] GenerateMaze(int mazeSize)
        {
            _size = mazeSize;
            InitCells();
            SetInitRelations();
            Generate();
            return _cells;
        }

        private void InitCells()
        {
            _cells = new Cell[_size][];
            for(int i=0; i<_size; i++)
            {
                _cells[i] = new Cell[_size];
                for(int j=0; j<_size; j++)
                {
                    _cells[i][j] = new Cell(i, j);
                }
            }
        }
        private void SetInitRelations()
        {
            for(int i=0; i<_size; i++)
            {
                for(int j=0; j<_size; j++)
                {
                    if (i != 0) CellSupport.SetRelation(_cells[i][j], _cells[i - 1][j], Directions.DirectionEnum.Up, false);
                    else CellSupport.SetRelation(_cells[i][j], null, Directions.DirectionEnum.Up, false);

                    if (i != _size - 1) CellSupport.SetRelation(_cells[i][j], _cells[i + 1][j], Directions.DirectionEnum.Down, false);
                    else CellSupport.SetRelation(_cells[i][j], null, Directions.DirectionEnum.Down, false);

                    if (j != 0) CellSupport.SetRelation(_cells[i][j], _cells[i][j - 1], Directions.DirectionEnum.Left, false);
                    else CellSupport.SetRelation(_cells[i][j], null, Directions.DirectionEnum.Left, false);

                    if (j != _size - 1) CellSupport.SetRelation(_cells[i][j], _cells[i][j + 1], Directions.DirectionEnum.Right, false);
                    else CellSupport.SetRelation(_cells[i][j], null, Directions.DirectionEnum.Right, false);
                }
            }
        }
        private void Generate()
        {
            int curLine = T.Next(_size);
            int curColumn = T.Next(_size);

            Stack<Cell> stackCells = new Stack<Cell>();
            
            var curCell = _cells[curLine][curColumn];
            curCell.Visited = true;
            while (HasNotVisitedCells()) // Пока есть непосещённые ячейки
            {
                var newCell = GetNotVisitedNeighbords(curCell);
                if(newCell != null) // Имеет непосещённых соседей
                {
                    stackCells.Push(curCell);

                    // Убираем между ячейками стены 
                    var rel = CellSupport.GetCellsRelation(curCell, newCell);
                    rel.CanMove = true;

                    rel = CellSupport.GetCellsRelation(newCell, curCell);
                    rel.CanMove = true;

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

        private bool HasNotVisitedCells()
        {
            for(int i=0; i<_size; i++)
            {
                for(int j=0;j<_size; j++)
                {
                    if (_cells[i][j].Visited == false) return true;
                }
            }

            return false;
        }

        private Cell GetNotVisitedNeighbords(Cell curCell)
        {
            List<Cell> neighbords = new List<Cell>();
            foreach(var relation in curCell._relations)
            {
                var nextCell = curCell.GetNextCell(relation.Key);
                if (nextCell != null && nextCell.Visited == false) neighbords.Add(nextCell);
            }

            if (neighbords.Count == 0) return null;
            else return neighbords[T.Next(neighbords.Count)];
        }


    }
}
