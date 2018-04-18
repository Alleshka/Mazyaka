using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze
{
    public class Human : LiveGameObject
    {
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
