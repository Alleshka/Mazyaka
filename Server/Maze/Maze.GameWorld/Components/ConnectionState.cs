using System;

namespace Maze.GameWorld.Components
{
    [Flags]
    public enum ConnectionConditions
    {
        None = 0,
        Destroyed = 1 << 0,
        // Collapsed = 1 << 1,  // rubble blocks passage even if destroyed
        // Frozen = 1 << 2,  // future: movement penalty
    }
}
