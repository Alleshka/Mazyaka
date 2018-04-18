using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze
{
    public interface IMazeGenerator
    {
        MazeStruct GenerateMazeStruct(int size);
        Cell[][] GenerateMazeCells(int size);
    }
}
