using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.MazeConnection
{
    /// <summary>
    /// Возможные команды
    /// </summary>
    public enum TypeCommand
    {
        // ServerTypes
        Responce,
        Error,
        // ClientTypes
        Login,
        CreateGame,
        JoinGame,
        SendStartMaze,
        SendStartPoint,
        MoveObject,
    }

    /// <summary>
    /// Передаваемый пакет данных
    /// </summary>
    [DataContract(IsReference = true)]
    public class PackCommand : TransmittedClass<PackCommand>
    {
        [DataMember]
        public TypeCommand Type { get; private set; } // Тип команды
        [DataMember]
        public List<String> Args { get; private set; } // список аргументов

        public String this[int index] => Args[index];

        public PackCommand(TypeCommand type)
        {
            this.Type = type;
            Args = new List<string>();
        }

        public void AddArgument(String arg)
        {
            Args.Add(arg);
        }
    }
}
