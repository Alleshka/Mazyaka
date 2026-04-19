using Maze.GameWorld.ComponentStore;
using Maze.GameWorld.Components;
using Maze.GameWorld.Events;
using System;
using System.Collections.Generic;

namespace Maze.GameWorld
{
    internal class MazeState
    {
        private int _nextEntityId = 1;
        private readonly Dictionary<Type, object> _componentStores = new Dictionary<Type, object>();

        private readonly Dictionary<int, ConnectionConditions> _connectionConditions = new Dictionary<int, ConnectionConditions>();

        public void AddConnectionCondition(int connectionId, ConnectionConditions condition)
        {
            _connectionConditions.TryGetValue(connectionId, out var existing);
            _connectionConditions[connectionId] = existing | condition;
        }

        public ConnectionConditions GetConnectionConditions(int connectionId)
            => _connectionConditions.TryGetValue(connectionId, out var c) ? c : ConnectionConditions.None;

        public Entity CreateEntity() => new(_nextEntityId++);

        public void Add<T>(Entity e, T component) where T : struct => GetStore<T>().Add(e, component);
        public bool Has<T>(Entity e) where T : struct => GetStore<T>().Has(e);

        private IComponentStore<T> GetStore<T>() where T : struct
        {
            var type = typeof(T);

            if (!_componentStores.TryGetValue(type, out var store))
            {
                store = new SimpleComponentStore<T>();
                _componentStores[type] = store;
            }

            return (IComponentStore<T>)store;
        }

        public ref T Get<T>(Entity e) where T : struct => ref GetStore<T>().Get(e);

        public void Remove<T>(Entity e) where T : struct => GetStore<T>().Remove(e);

        public void ClearIntents()
        {
            GetStore<MoveIntent>().Clear();
            GetStore<DestroyWallIntent>().Clear();
        }

        public void ClearEvents()
        {
            GetStore<MoveSuccessEvent>().Clear();
            GetStore<MoveExitEvent>().Clear();
            GetStore<MoveBlockedNoConnectionEvent>().Clear();
            GetStore<MoveBlockedByBlockerEvent>().Clear();
            GetStore<WallDestroyedEvent>().Clear();
            GetStore<DestroyFailedEvent>().Clear();
        }

        public IEnumerable<Entity> Query<T>() where T : struct
        {
            var store = GetStore<T>();
            foreach (var kvp in store.All())
            {
                yield return kvp.Key;
            }
        }

        public IEnumerable<(Entity, T1, T2)> Query<T1, T2>()
            where T1 : struct
            where T2 : struct
        {
            var p1 = GetStore<T1>();
            var p2 = GetStore<T2>();

            foreach (var (e, c1) in p1.All())
            {
                if (p2.Has(e))
                {
                    yield return (e, c1, p2.Get(e));
                }
            }
        }

        public IEnumerable<(Entity, T1, T2, T3)> Query<T1, T2, T3>()
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var p1 = GetStore<T1>();
            var p2 = GetStore<T2>();
            var p3 = GetStore<T3>();

            foreach (var (e, c1) in p1.All())
            {
                if (p2.Has(e) && p3.Has(e))
                {
                    yield return (e, c1, p2.Get(e), p3.Get(e));
                }
            }
        }
    }
}
