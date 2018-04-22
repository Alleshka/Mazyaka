using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Maze.Effects;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze.GameObjects
{
    [DataContract]
    public class Exit : BaseGameObject
    {
        public Exit(MazePoint point) : base(point)
        {

        }

        public Exit(int line, int column) : base(line, column)
        {

        }

        public override void Action(BaseGameObject obj)
        {
            if(obj is Human)
            {

                // Вешаем эффект победителя
                (obj as Human).AddEffect(new WinEffect());
            }
        }
    }
}
