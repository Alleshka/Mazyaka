using MazeSolution.Core.MazeStructrure;
using System;

namespace MazeSolution.Core.Generators
{
    public interface IMazeGenerator
    {
        IMazeStructure GenerateMaze(int lineCount, int columnCount);
    }
}
