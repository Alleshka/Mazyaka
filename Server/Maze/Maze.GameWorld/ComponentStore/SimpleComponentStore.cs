using System.Collections.Generic;

namespace Maze.GameWorld.ComponentStore
{
    internal class SimpleComponentStore<T> : IComponentStore<T>
    {
        private readonly Dictionary<EcsEntity, T> _data = new Dictionary<EcsEntity, T>();

        public void Add(EcsEntity e, T component) => _data[e] = component;
        public void Set(EcsEntity e, T component) => _data[e] = component;

        public T Get(EcsEntity e) => _data[e]; // ref CollectionsMarshal.GetValueRefOrNullRef(_data, e);

        public bool Has(EcsEntity e) => _data.ContainsKey(e);

        public void Remove(EcsEntity e) => _data.Remove(e);

        public IEnumerable<KeyValuePair<EcsEntity, T>> All() => _data;

        public void Clear() => _data.Clear();
    }
}
