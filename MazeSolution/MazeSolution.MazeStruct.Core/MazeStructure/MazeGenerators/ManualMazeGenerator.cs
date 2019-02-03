using System;
using System.Collections.Generic;
using System.Text;
using static MazeSolution.MazeStruct.Core.MazeStructure.CellSupport;

namespace MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators
{
    public class ManualMazeGenerator : IMazeGenerator
    {
        public Cell[][] GenerateMaze(int mazeSize)
        {
            // if (mazeSize != 2) throw new Exception("Данный генератор тестовый, не подходит для генерации");

            Cell[][] _cells = new Cell[mazeSize][];
            for(int i=0; i<mazeSize; i++)
            {
                _cells[i] = new Cell[mazeSize];
                for(int j = 0; j<mazeSize; j++)
                {
                    _cells[i][j] = new Cell();
                }
            }

            if (mazeSize >= 2)
            {
                SetRelation(_cells[0][0], null, Directions.DirectionEnum.Up, false);
                SetRelation(_cells[0][0], _cells[0][1], Directions.DirectionEnum.Right, true);
                SetRelation(_cells[0][0], _cells[1][0], Directions.DirectionEnum.Down, false);
                SetRelation(_cells[0][0], null, Directions.DirectionEnum.Left, false);


                SetRelation(_cells[0][1], null, Directions.DirectionEnum.Up, false);
                SetRelation(_cells[0][1], _cells[0][0], Directions.DirectionEnum.Left, true);
                SetRelation(_cells[0][1], _cells[1][1], Directions.DirectionEnum.Down, true);
                SetRelation(_cells[0][1], null, Directions.DirectionEnum.Right, false);

                SetRelation(_cells[1][0], _cells[0][0], Directions.DirectionEnum.Up, false);
                SetRelation(_cells[1][0], _cells[1][1], Directions.DirectionEnum.Right, true);
                SetRelation(_cells[1][0], null, Directions.DirectionEnum.Down, false);
                SetRelation(_cells[1][0], null, Directions.DirectionEnum.Left, false);

                SetRelation(_cells[1][1], _cells[0][1], Directions.DirectionEnum.Up, true);
                SetRelation(_cells[1][1], _cells[1][0], Directions.DirectionEnum.Left, true);
                SetRelation(_cells[1][1], null, Directions.DirectionEnum.Down, false);
                SetRelation(_cells[1][1], null, Directions.DirectionEnum.Right, false);
            }

            if(mazeSize >= 3)
            {
                SetRelation(_cells[0][1], _cells[0][2], Directions.DirectionEnum.Right, false);

                SetRelation(_cells[0][2], _cells[0][1], Directions.DirectionEnum.Left, false);
                SetRelation(_cells[0][2], null, Directions.DirectionEnum.Up, false);
                SetRelation(_cells[0][2], null, Directions.DirectionEnum.Right, false);
                SetRelation(_cells[0][2], _cells[1][2], Directions.DirectionEnum.Down, true);

                SetRelation(_cells[1][0], _cells[2][0], Directions.DirectionEnum.Down, true);

                SetRelation(_cells[1][1], _cells[2][1], Directions.DirectionEnum.Down, true);
                SetRelation(_cells[1][1], _cells[1][2], Directions.DirectionEnum.Right, false);


                SetRelation(_cells[1][2], _cells[0][2], Directions.DirectionEnum.Up, true);
                SetRelation(_cells[1][2], _cells[1][1], Directions.DirectionEnum.Left, false);
                SetRelation(_cells[1][2], null, Directions.DirectionEnum.Right, false);
                SetRelation(_cells[1][2], null, Directions.DirectionEnum.Down, false);

                SetRelation(_cells[2][0], _cells[1][0], Directions.DirectionEnum.Up, true);
                SetRelation(_cells[2][0], null, Directions.DirectionEnum.Down, false);
                SetRelation(_cells[2][0], null, Directions.DirectionEnum.Left, false);
                SetRelation(_cells[2][0], _cells[2][1], Directions.DirectionEnum.Right, true);

                SetRelation(_cells[2][1], _cells[2][0], Directions.DirectionEnum.Left, true);
                SetRelation(_cells[2][1], _cells[1][0], Directions.DirectionEnum.Up, true);
                SetRelation(_cells[2][1], null, Directions.DirectionEnum.Down, false);
                SetRelation(_cells[2][1], _cells[2][2], Directions.DirectionEnum.Right, true);

                SetRelation(_cells[2][2], _cells[1][2], Directions.DirectionEnum.Up, true);
                SetRelation(_cells[2][2], _cells[2][1], Directions.DirectionEnum.Left, true);
                SetRelation(_cells[2][2], null, Directions.DirectionEnum.Right, false);
                SetRelation(_cells[2][2], null, Directions.DirectionEnum.Down, false);
            }

            return _cells;
        }
    }
}
