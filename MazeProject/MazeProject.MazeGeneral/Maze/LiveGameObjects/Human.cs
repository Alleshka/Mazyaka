using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    [DataContract]
    public class Human : LiveGameObject
    {

        private Human() : base(-1, -1)
        {

        }

        public Human(int line, int point) : base(line, point)
        {

        }

        public Human(MazePoint point) : base(point)
        {

        }

        public override void Action(BaseGameObject obj)
        {
            throw new NotImplementedException();
        }
    }
}
