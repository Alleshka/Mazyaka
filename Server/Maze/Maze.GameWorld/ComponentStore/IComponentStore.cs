using System.Collections.Generic;

namespace Maze.GameWorld.ComponentStore
{
    internal interface IComponentStore<T>
    {
        void Add(EcsEntity e, T component);
        void Set(EcsEntity e, T component);
        T Get(EcsEntity e);
        bool Has(EcsEntity e);
        void Remove(EcsEntity e);
        IEnumerable<KeyValuePair<EcsEntity, T>> All();
        void Clear();
    }
}
