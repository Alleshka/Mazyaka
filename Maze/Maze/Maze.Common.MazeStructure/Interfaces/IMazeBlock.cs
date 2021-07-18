using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;
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
    public interface IMazeBlock : IBaseMazeObject
    {
        // TODO: Нужен какой-нибудь MoveResult, который будет отвечать удалось ли передвинуться
        public void MoveObject(ILiveGameObject gameObject, IMazeDirection direction);
    }
}
