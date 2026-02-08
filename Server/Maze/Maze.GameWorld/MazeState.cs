using Maze.GameWorld.ComponentStore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Maze.GameWorld
{
    public class MazeState
    {
        private int _nextEntityId = 1;
        private readonly Dictionary<Type, IComponentStore> _componentStores = new Dictionary<Type, IComponentStore>();

        public Entity CreateEntity()
        {
            return new Entity(_nextEntityId++);
        }

        public void Add<T>(Entity e, T component) where T : struct
        {
            var store = GetStore<T>();
            store.Data[e.Id] = component;
        }

        public bool Has<T>(Entity e) where T : struct
        {
            var store = GetStore<T>();
            return store.Data.ContainsKey(e.Id);
        }

        private SimpleComponentStore<T> GetStore<T>() where T : struct
        {
            var type = typeof(T);

            if (!_componentStores.TryGetValue(type, out var store))
            {
                store = new SimpleComponentStore<T>();
                _componentStores[type] = store;
            }

            return (SimpleComponentStore<T>)store;
        }

        public ref T Get<T>(Entity e) where T : struct
        {
            var store = GetStore<T>();
            return ref CollectionsMarshal.GetValueRefOrNullRef(store.Data, e.Id);
        }


        public void Remove<T>(Entity e) where T : struct
        {
            var store = GetStore<T>();
            store.Data.Remove(e.Id);
        }

        public IEnumerable<Entity> Query<T>() where T : struct
        {
            var store = GetStore<T>();
            foreach (var kvp in store.Data)
            {
                yield return new Entity(kvp.Key);
            }
        }

        public IEnumerable<Entity> Query<T1, T2>()
            where T1 : struct
            where T2 : struct
        {
            var a = GetStore<T1>().Data;
            var b = GetStore<T2>().Data;

            if (a.Count > b.Count)
            {
                return Query(b, a);
            }
            else
            {
                return Query(a, b);
            }
        }

        private IEnumerable<Entity> Query<T1, T2>(Dictionary<int, T1> a,
            Dictionary<int, T2> b)
        {
            foreach (var id in a.Keys)
                if (b.ContainsKey(id))
                    yield return new Entity(id);
        }
    }
}
