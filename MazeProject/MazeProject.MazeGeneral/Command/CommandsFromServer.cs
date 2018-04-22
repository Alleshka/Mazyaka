using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    /// <summary>
    /// Сообзает пользователю, что его ход
    /// </summary>
    [DataContract]
    public class YourStep : AbstractResponse
    {
    }

    /// <summary>
    /// Сообщает, что сервер готов принять лабиринт
    /// </summary>
    [DataContract]
    public class GiveMaze : AbstractResponse
    {
    }

    /// <summary>
    /// Сообщает, что готов принять начальную точку
    /// </summary>
    [DataContract]
    public class GivePoint : AbstractResponse
    {

    }

    [DataContract]
    public class GameFinished : AbstractResponse
    {
        [DataMember]
        public Guid Winner { get; set; }
        
        [DataMember]
        public Maze.MazeStruct FullMaze { get; set; }

        public GameFinished(Guid id, Maze.MazeStruct fullMaze) : base()
        {
            Winner = id;
            FullMaze = fullMaze;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.ToString());
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"[Winner:{Winner.ToString()}");
            return stringBuilder.ToString();
        }
    }
}
