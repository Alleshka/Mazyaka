using System;
using System.Collections.Generic;
using System.Text;
using MazeProject.General.Mazes.MazeStructure;

namespace MazeProject.General.Mazes.MazeGenerator
{
    public interface IMazeGenerator
    {
        MazeStruct Generate(int n);
    }
}
