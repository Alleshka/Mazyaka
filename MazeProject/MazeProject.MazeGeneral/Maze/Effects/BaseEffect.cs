using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze.Effects
{
    [DataContract]
    public abstract class BaseEffect
    {
        [DataMember]
        public String Description { get; set; }
        public BaseEffect(String descr)
        {
            Description = descr;
        }
    }
}
