﻿using Maze.Common.MazeStructure.Directions;
using Maze.Common.MazeStructure.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Common.MazeStructure
{
    public abstract class BaseBlock : BaseMazeObject, IMazeBlock
    {
        public virtual void MoveObject(ILiveGameObject gameObject, IMazeDirection direction)
        {

        }
    }
}
