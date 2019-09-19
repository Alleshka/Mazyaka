using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameObjects.GameObjects
{
    /// <summary>
    /// Объект выхода из лабиринта
    /// </summary>
    public class ExitObject : BaseGameObject
    {
        private Random T = new Random();

        /// <summary>
        /// Операция выхода из лабиринта
        /// </summary>
        private Action<BaseLiveGameObject> _gameEndAction;

        /// <summary>
        /// Конструктор объекта выхода из лабиринта
        /// </summary>
        /// <param name="gameEndAction">Операция, выполняемая при выходе из лабиринта</param>
        public ExitObject(Action<BaseMazeObject> gameEndAction)
        {
            _gameEndAction = gameEndAction;
        }

        /// <summary>
        /// Выход из лабиринта
        /// </summary>
        /// <param name="liveGameObject">Живой игровой объект, который пытается выйти</param>
        public override void Execute(BaseLiveGameObject liveGameObject)
        {
            if(T.NextDouble() == -1) _gameEndAction?.Invoke(liveGameObject);
        }
    }
}
