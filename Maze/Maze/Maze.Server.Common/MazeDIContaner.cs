using Autofac;

namespace Maze.Server.Common
{
    public class MazeDIContaner
    {
        #region autofac
        private static readonly ContainerBuilder _builder = new ContainerBuilder();
        private static Lazy<IContainer> _lazy = new Lazy<IContainer>(() => _builder.Build());
        private static IContainer Container => _lazy.Value;
        #endregion

        public static void RegisterType<T, V>(bool isSingleTone = false)
            where T : notnull
            where V : notnull
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

        public static void RegisterType<T>(Func<IComponentContext, T> func, bool singletone = false)
            where T : notnull
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

        public static void RegisterType<T>(T obj)
            where T : class
        {
            _builder.RegisterInstance(obj).As<T>();
        }

        public static T Get<T>()
            where T : notnull
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var implementation = scope.Resolve<T>();
                return implementation;
            }
        }
    }
}