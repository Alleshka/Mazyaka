using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze.Effects
{
    public abstract class BaseEffect
    {
        public String Description { get; set; }
        public BaseEffect(String descr)
        {
            Description = descr;
        }
    }
}
