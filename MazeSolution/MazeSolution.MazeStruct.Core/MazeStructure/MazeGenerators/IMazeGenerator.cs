using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.MazeStructure.MazeGenerators
{
    public interface IMazeGenerator
    {
        Cell[][] GenerateMaze(int mazeSize);
    }
}
