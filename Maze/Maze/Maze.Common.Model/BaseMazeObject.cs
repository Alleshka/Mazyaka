using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Common.Model
{
    public abstract class BaseMazeObject
    {
        public Guid ObjectID { get; protected set; }

        public BaseMazeObject(Guid objectID)
        {
            ObjectID = objectID;
        }

        public BaseMazeObject() : this(Guid.NewGuid())
        {

        }
    }
}
