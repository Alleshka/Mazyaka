using System;

namespace Maze.GameWorld.Components
{
    [Flags]
    public enum ConnectionConditions
    {
        None = 0,
        Destroyed = 1 << 0,
        Sealed = 1 << 1,      // permanently impassable; applies to any connection type
        // Collapsed = 1 << 2,  // rubble blocks passage even if destroyed
        // Frozen = 1 << 3,  // future: movement penalty
    }
}
