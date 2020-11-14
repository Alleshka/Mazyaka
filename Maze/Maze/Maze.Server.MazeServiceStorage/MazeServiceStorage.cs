using Maze.Server.MazeService;
using System;
using System.Collections.Generic;

namespace Maze.Server.ServiceStorage
{
    /// <summary>
    /// Хранилище всех сервисов, используемых в проекте
    /// Скорее всего реализация меняться не будет
    /// </summary>
    public class MazeServiceStorage
    {
        private static MazeServiceStorage _instance;
        public static MazeServiceStorage Instance
        {
            get
            {
                return _instance ?? (_instance = new MazeServiceStorage());
            }
        }

        private Dictionary<Type, IMazeService> _services;
        private MazeServiceStorage()
        {
            _services = new Dictionary<Type, IMazeService>();
        }

        public void AddService(Type type, IMazeService service)
        {
            _services[type] = service;
        }
        public void AddService<T>(IMazeService service)
        {
            AddService(typeof(T), service);
        }

        public IMazeService GetService(Type type)
        {
            _services.TryGetValue(type, out var resultService);
            if (resultService == null)
            {
                throw new Exception($"Не удалось найти сервис с типом {type}");
            }
            return resultService;
        }
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }
    }
}
