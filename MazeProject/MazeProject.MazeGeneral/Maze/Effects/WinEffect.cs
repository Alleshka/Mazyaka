using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze.Effects
{
    /// <summary>
    /// Добавляется к победившему пользователю
    /// </summary>
    [DataContract]
    public class WinEffect : BaseEffect
    {
        public WinEffect() : base("Пользователь выиграл матч")
        {

        }

        public override bool Equals(object obj)
        {
            if (obj is WinEffect) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Description;
        }

    }
}
