using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Maze.GameWorld.ComponentStore
{
    internal class SimpleComponentStore<T> : IComponentStore<T> where T : struct
    {
        private readonly Dictionary<Entity, T> _data = new Dictionary<Entity, T>();

        public void Add(Entity e, T component) => _data[e] = component;

        public ref T Get(Entity e) => ref CollectionsMarshal.GetValueRefOrNullRef(_data, e);

        public bool Has(Entity e) => _data.ContainsKey(e);

        public void Remove(Entity e) => _data.Remove(e);

        public IEnumerable<KeyValuePair<Entity, T>> All() => _data;

        public void Clear() => _data.Clear();
    }
}
