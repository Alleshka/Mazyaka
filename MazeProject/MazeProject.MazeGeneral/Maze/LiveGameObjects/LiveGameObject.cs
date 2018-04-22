using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeProject.MazeGeneral.Maze.Effects;

namespace MazeProject.MazeGeneral.Maze
{
    // От данного класса наследуются все живые объекты
    public abstract class LiveGameObject : BaseGameObject
    {
        private List<BaseEffect> effectsList;
        public List<BaseEffect> Effects
        {
            get => new List<BaseEffect>(effectsList);
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
