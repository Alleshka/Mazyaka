using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General.Mazes.GameObjects
{
    public abstract class BaseGameObject
    {
        public Guid ID { get; private set; }

        public abstract void Action(BaseGameObject gameObject);
    }
}
