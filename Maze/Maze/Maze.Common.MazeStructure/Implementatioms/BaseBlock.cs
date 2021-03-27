﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure
{
    public abstract class BaseBlock : IMazeBlock
    {
        public virtual bool CanMove => false;

        public virtual bool CanDestroy => false;

        public virtual void MoveAction()
        {

        }
    }
}
