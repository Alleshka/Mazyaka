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


    /// <summary>
    /// Пакет с несколькими командами, чтобы послать 1 сообщением
    /// </summary>
    [DataContract]
    public class CumulativeResponse : AbstractResponse
    {
        public List<AbstractResponse> ResponseList;

        public CumulativeResponse() : base()
        {
            ResponseList = new List<AbstractResponse>();
        }

        public void AddResponse(AbstractResponse response)
        {
            ResponseList.Add(response);
        }
    }

    [DataContract]
    public class GameFinished : AbstractResponse
    {
        [DataMember]
        public Guid Winner { get; set; }

        public GameFinished(Guid id) : base()
        {
            Winner = id;
        }
    }
}
