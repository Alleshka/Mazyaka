using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject.MazeGeneral.Maze
{
    // От данного класса наследуются все живые объекты
    public abstract class LiveGameObject : BaseGameObject
    {
        public LiveGameObject(int line, int column) : base(line, column)
        {

        }

        public LiveGameObject(MazePoint point) : base(point)
        {

        }

        public abstract override void Action(BaseGameObject obj);
    }
}
