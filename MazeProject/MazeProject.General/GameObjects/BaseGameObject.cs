using System;
using System.Collections.Generic;
using System.Text;

namespace MazeProject.General.GameObjects
{
    public abstract class BaseGameObject
    {
        public Guid ObjectID { get; private set; }

        public BaseGameObject()
        {
            ObjectID = Guid.NewGuid();
        }
    }
}
