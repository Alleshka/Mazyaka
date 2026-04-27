using System.Collections.Generic;

namespace Maze.GameWorld.ComponentStore
{
    internal interface IComponentStore<T> where T : struct
    {
        void Add(Entity e, T component);
        void Set(Entity e, T component);
        T Get(Entity e);
        bool Has(Entity e);
        void Remove(Entity e);
        IEnumerable<KeyValuePair<Entity, T>> All();
        void Clear();
    }
}
