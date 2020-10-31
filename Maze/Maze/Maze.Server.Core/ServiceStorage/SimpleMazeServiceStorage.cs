using Maze.Server.MazeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Server.Core.ServiceStorage
{
    public static class SimpleMazeServiceStorage
    {
        private static Dictionary<Type, IMazeService> _services;
       
        static SimpleMazeServiceStorage()
        {
        }

        private static void AddService(Type type, IMazeService service)
        {
            _services[type] = service;
        }

        public static IMazeService GetService(Type type)
        {
            _services.TryGetValue(type, out var resultService);
            return resultService;
        }

        public static IMazeService GetService<T>()
        {
            return GetService(typeof(T));
        }

        private static void AddService<T>(IMazeService service)
        {
            AddService(typeof(T), service);
        }
    }
}
