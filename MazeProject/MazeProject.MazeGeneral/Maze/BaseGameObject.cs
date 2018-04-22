using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    [DataContract]
    public abstract class BaseGameObject
    {
        //[DataMember]
        public Guid ObjectID { get; set; }
        /// <summary>
        /// Текущий адрес объекта
        /// </summary>
        /// 

        [DataMember]
        private MazePoint curAddres;
        public MazePoint CurAddres
        {
            get
            {
                return curAddres.Clone() as MazePoint;
            }
            set
            {
                curAddres = value;
            }

        }
        public abstract void Action(BaseGameObject obj);

        public BaseGameObject()
        {

        }

        public BaseGameObject(int line, int column)
        {
            ObjectID = Guid.NewGuid();
            CurAddres = new MazePoint(line, column);
        }

        public BaseGameObject(MazePoint point)
        {
            ObjectID = Guid.NewGuid();
            CurAddres = point;
        }
    }
}
