using Maze.Common.MazaPackages;
using Maze.Common.MazaPackages.Packages;
using Maze.Server.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void HandlePackage(IMazePackage package);
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
        protected abstract void Handle(T package);

        public void HandlePackage(IMazePackage package)
        {
            if (IsHandable(package))
            {
                Handle(package as T);
            }
            else
            {
                GoNext(package);
            }
        }

        /// <summary>
        /// Переход к следующему элементу цепочки
        /// </summary>
        /// <param name="package"></param>
        private void GoNext(IMazePackage package)
        {
            if (NextHandler != null)
            {
                NextHandler.HandlePackage(package);
            }
            else throw new Exception("Неизвестный пакет");
        }

        /// <summary>
        /// Может ли конкретный элемент цепочки обработать данный пакет
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        protected virtual bool IsHandable(IMazePackage package)
        {
            return package.TypeName == (typeof(T)).ToString().Split(".").Last();
        }
    }
}
