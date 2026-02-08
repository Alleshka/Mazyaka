using Maze.MazeStructure.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.MazeStructure
{
    internal class SimpleMazeInfo : IMazeInfo
    {
        public IMaze MazeStructure { get; }

        public IMazeMetadata Metadata { get; }

        public SimpleMazeInfo(IMaze mazeStructure, IMazeMetadata metadata)
        {
            MazeStructure = mazeStructure;
            Metadata = metadata;
        }
    }
}
