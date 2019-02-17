using MazeSolution.MazeStruct.Core.GameObjects.LiveGameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.MazeStruct.Core.GameObjects.GameObjects
{
    public class ExitGameObject : BaseGameObject
    {
        public ExitGameObject() : base()
        {
            this._executeList[typeof(HumanGameObject)] = HumanHandler;
        }

        private void HumanHandler(BaseGameObject obj)
        {
            var human = obj as HumanGameObject;
            throw new Exception("Конец игры");
        }
    }
}
