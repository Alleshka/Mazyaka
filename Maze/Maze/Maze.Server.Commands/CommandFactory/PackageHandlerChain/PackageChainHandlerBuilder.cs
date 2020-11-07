using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Server.Core.PackageHandlerChain
{
    /// <summary>
    /// Построитель цепочки исполнителей
    /// </summary>
    internal class PackageChainHandlerBuilder
    {
        /// <summary>
        /// Начало цепочки
        /// </summary>
        public IMazePackageHandler Head { get; private set; }

        /// <summary>
        /// Конец цепочки
        /// </summary>
        private IMazePackageHandler Last { get; set; }

        public void AddChain(IMazePackageHandler handler)
        {
            if (Head == null)
            {
                Head = handler;
                Last = handler;
            }
            else
            {
                Last.NextHandler = handler;
                Last = Last.NextHandler;
            }
        }
    }
}
