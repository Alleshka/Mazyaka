using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure
{
    /// <summary>
    /// Базовый блок лабиринта
    /// </summary>
    public interface IMazeBlock
    {
        public bool CanMove { get; }

        public bool CanDestroy { get; }

        // TODO: Передавать сюда GameObject, который ходит
        public void MoveAction();
    }
}
