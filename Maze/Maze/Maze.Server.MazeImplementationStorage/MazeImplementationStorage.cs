using System;
using System.Collections.Generic;

namespace Maze.Server.ImplementationStorage
{
    /// <summary>
    /// Хранилище всех реализаций
    /// </summary>
    public class MazeImplementationStorage
    {
        private static MazeImplementationStorage _instance;
        public static MazeImplementationStorage Instance
        {
            get
            {
                return _instance ?? (_instance = new MazeImplementationStorage());
            }
        }

        private Dictionary<Type, ImplementationItem> _services;
        private MazeImplementationStorage()
        {
            _services = new Dictionary<Type, ImplementationItem>();
        }

        public void Bind(Type type, Func<object> obj, ImplementationMode mode = ImplementationMode.AlwaysNew)
        {
            _services[type] = new ImplementationItem(mode, obj);
        }

        public void Bind(Type type, object obj, ImplementationMode mode = ImplementationMode.AlwaysNew)
        {
            Bind(type, () => obj, mode);
        }

        public void Bind<T> (Func<T> obj, ImplementationMode mode = ImplementationMode.AlwaysNew)
        {
            Bind(typeof(T), obj, mode);
        }

        public void Bind<T>(T obj, ImplementationMode mode = ImplementationMode.AlwaysNew)
        {
            Bind(typeof(T), () => obj, mode);
        }

        public object GetImplementation(Type type)
        {
            _services.TryGetValue(type, out var resultService);
            if (resultService == null)
            {
                throw new Exception($"Не удалось найти реализацию с типом {type}");
            }
            return resultService.Result;
        }
        public T GetImplementation<T>()
        {
            return (T)GetImplementation(typeof(T));
        }
    }

    public enum ImplementationMode
    {
        Single,
        AlwaysNew
    }

    class ImplementationItem
    {
        private ImplementationMode _implementationMode;
        private Func<object> _imlementationAction;

        private object _implementationResult;
        public object Result
        {
            get
            {
                if (_implementationResult == null || _implementationMode == ImplementationMode.AlwaysNew)
                {
                    _implementationResult = _imlementationAction();
                }
                return _implementationResult;
            }
        }

        public ImplementationItem(ImplementationMode mode, Func<object> func)
        {
            _implementationMode = mode;
            _imlementationAction = func;
        }
    }
}
