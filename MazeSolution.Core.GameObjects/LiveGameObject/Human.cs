using System;
using System.Collections.Generic;
using System.Text;

namespace MazeSolution.Core.GameObjects.LiveGameObject
{
    /// <summary>
    /// Объект человека в лабиринте
    /// </summary>
    public class Human : BaseLiveGameObject
    {
        public Human()
        {
            // TODO: Конфиг
            HealthPoints = 10;
            ActionPoints = 1;
        }

        public override void Execute(BaseLiveGameObject liveGameObject)
        {
            
        }
    }
}
