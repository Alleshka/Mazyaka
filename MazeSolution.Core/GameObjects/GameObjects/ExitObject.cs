using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameObjects.GameObjects
{
    public class ExitObject : BaseGameObject
    {
        private Action<BaseLiveGameObject> _gameEndAction;
        public ExitObject(Action<BaseLiveGameObject> gameEndAction)
        {
            _gameEndAction = gameEndAction;
        }

        public override void Execute(BaseLiveGameObject liveGameObject)
        {
            _gameEndAction?.Invoke(liveGameObject);
        }
    }
}
