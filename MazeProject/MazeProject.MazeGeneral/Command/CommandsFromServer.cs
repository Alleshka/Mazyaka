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
    public class YourStep : AbstractMessage
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
}
