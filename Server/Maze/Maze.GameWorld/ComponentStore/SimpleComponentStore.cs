using System.Collections.Generic;

namespace Maze.GameWorld.ComponentStore
{
    internal class SimpleComponentStore<T> : IComponentStore where T : struct
    {
        public readonly Dictionary<int, T> Data = new Dictionary<int, T>();
    }
}
