using Autofac;
using Maze.Server.MazeService;
using System;

namespace Maze.Server.AutofacContainer
{
    public class MazeAutofacContainer
    {
        private static MazeAutofacContainer _instance;
        public static MazeAutofacContainer Instance
        {
            get
            {
                return _instance ??= new MazeAutofacContainer();
            }
        }

        private IContainer _container;
        public IContainer Container
        {
            get
            {
                if (_container == null) Build();
                return _container;
            }
        }

        private readonly ContainerBuilder _builder = new ContainerBuilder();

        public void Build()
        {
            _container = _builder.Build();
        }

        public void AddService<T>(IMazeService service)
        {
            var type = service.GetType();
            _builder.RegisterType(type).As<T>().SingleInstance();
        }

        public void AddImplementation<T, V>(bool isSingleTone = false)
        {
            if (isSingleTone)
            {
                _builder.RegisterType<T>().As<V>();
            }
            else
            {
                _builder.RegisterType<T>().As<V>().SingleInstance();
            }
        }

        public void AddImplementation<T>(Func<IComponentContext, T> func, bool singletone = false)
        {
            if (singletone)
            {
                _builder.Register(c => func(c));
            }
            else
            {
                _builder.Register(c => func(c)).SingleInstance();
            }
        }

        public void AddImplementation<T>(T obj) where T : class
        {
            _builder.RegisterInstance(obj).As<T>();
        }

        public IMazeService GetService(Type type)
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var components = scope.ComponentRegistry.Registrations;
                var service = scope.Resolve(type) as IMazeService;
                return service;
            }
        }
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public T GetImplementation<T>()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var implementation = scope.Resolve<T>();
                return implementation;
            }
        }
    }
}
