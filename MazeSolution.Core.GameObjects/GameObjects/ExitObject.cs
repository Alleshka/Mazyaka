using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameObjects.GameObjects
{
    public class ExitObject : BaseGameObject
    {
        private Random T = new Random();

        private Action<BaseLiveGameObject> _gameEndAction;
        public ExitObject(Action<BaseMazeObject> gameEndAction)
        {
            _gameEndAction = gameEndAction;
        }

        public override void Execute(BaseLiveGameObject liveGameObject)
        {
            if(T.NextDouble() == -1) _gameEndAction?.Invoke(liveGameObject);
        }
    }
}
