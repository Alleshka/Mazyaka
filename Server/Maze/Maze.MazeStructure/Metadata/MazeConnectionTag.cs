using System;

namespace Maze.MazeStructure.Metadata
{
    [Flags]
    public enum  ConnectionTypeTag
    {
        None = 0,
        Passage = 1 << 0,
        Boundary = 1 << 1,
        Exit = 1 << 2
    }

    [Flags]
    public enum ConnectionDirectionTag
    {
        None = 0,
        Left = 1 << 0,
        Up = 1 << 1,
        Right = 1 << 2,
        Down = 1 << 3
    }
}
