using Maze.Common.MazePackages;
using Maze.Server.Commands;
using Maze.Server.Commands.Commands;
using System;
using System.Linq;

namespace Maze.Server.Core.PackageHandlerChain
{
    /// <summary>
    /// Базовый интерфейс обработчика пакета
    /// Принимает пакет, выполняет команды
    /// </summary>
    internal interface IMazePackageHandler
    {
        /// <summary>
        /// Переход к следующему обработчику
        /// </summary>
        IMazePackageHandler NextHandler { get; set; }

        /// <summary>
        /// Обработка пакета
        /// </summary>
        /// <param name="package">Обрабатываемый пакет</param>
        IMazeServerCommand HandlePackage(IMazePackage package);
    }

    /// <summary>
    /// Базовый элемент цепочки обязанностией
    /// </summary>
    /// <typeparam name="T">Обрабатываемой элементом пакет</typeparam>
    internal abstract class BasePackageHandler<T> : IMazePackageHandler where T : class, IMazePackage
    {
        public IMazePackageHandler NextHandler { get; set; }

        /// <summary>
        /// Типизированная обработка пакета
        /// </summary>
        /// <param name="package">Обрабатываемый пакет</param>
        protected abstract IMazeServerCommand Handle(T package);

        public IMazeServerCommand HandlePackage(IMazePackage package)
        {
            if (IsHandable(package))
            {
                return Handle(package as T);
            }
            else
            {
                return GoNext(package);
            }
        }

        /// <summary>
        /// Переход к следующему элементу цепочки
        /// </summary>
        /// <param name="package"></param>
        private IMazeServerCommand GoNext(IMazePackage package)
        {
            if (NextHandler != null)
            {
                return NextHandler.HandlePackage(package);
            }
            else return new ExceptionCommand($"Неизвестный пакет {package.TypeName} {Environment.NewLine} {package}");
        }

        /// <summary>
        /// Может ли конкретный элемент цепочки обработать данный пакет
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        protected virtual bool IsHandable(IMazePackage package)
        {
            return package.TypeName == (typeof(T)).ToString().Split('.').Last();
        }
    }
}
