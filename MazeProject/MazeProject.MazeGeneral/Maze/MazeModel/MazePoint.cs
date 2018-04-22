using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Maze
{
    /// <summary>
    /// Адрес в лабиринте
    /// </summary>
    [DataContract]
    public class MazePoint : ICloneable
    {
        [DataMember]
        private int line;
        [DataMember]
        private int column;

        public int Line
        {
            get => line;
            set
            {
                if (value < -10) throw new Exception("Недопустимое значение Line");
                else
                {
                    line = value;
                }
            }
        }
        public int Column
        {
            get => column;
            set
            {
                if (value < -10) throw new Exception("Недопустимое значение Line");
                else
                {
                    column = value;
                }
            }
        }

        public MazePoint()
        {
            line = -1;
            column = -1;
        }

        public MazePoint(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public object Clone()
        {
            return new MazePoint(line, column);
        }
    }
}
