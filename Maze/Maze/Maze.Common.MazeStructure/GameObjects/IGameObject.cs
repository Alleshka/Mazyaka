using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure.GameObjects
{
    /// <summary>
    /// Игровой объект
    /// </summary>
    public interface IGameObject : IBaseMazeObject
    {
        void Execute(ILiveGameObject liveGameObject);
    }

    /// <summary>
    /// Живой игровой объект
    /// </summary>
    public interface ILiveGameObject : IGameObject
    {
        int Health { get; }
    }
}
