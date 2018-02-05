using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.GameModel
{
    /// <summary>
    /// Координаты в лабиринте
    /// </summary>
    [DataContract(IsReference = true)]
    public class Point
    {
        [DataMember]
        public int Line { get; set; }
        [DataMember]
        public int Column { get; set; }

        public Point(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }
}
