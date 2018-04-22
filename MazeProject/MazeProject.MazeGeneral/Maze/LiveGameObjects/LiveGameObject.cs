using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Maze.Effects;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    // От данного класса наследуются все живые объекты
    [DataContract]
    public abstract class LiveGameObject : BaseGameObject
    {
        [DataMember]
        private List<BaseEffect> effectsList;
        public List<BaseEffect> Effects
        {
            get
            {
                if (effectsList != null) return new List<BaseEffect>(effectsList);
                else return new List<BaseEffect>();
            }
        }

        private LiveGameObject() : base(-1, -1)
        {

        }

        public LiveGameObject(int line, int column) : base(line, column)
        {
            effectsList = new List<BaseEffect>();
        }

        public LiveGameObject(MazePoint point) : this(point.Line, point.Column)
        {
        }

        public void AddEffect(BaseEffect effect)
        {
            effectsList.Add(effect);
        }

        public abstract override void Action(BaseGameObject obj);
    }
}
