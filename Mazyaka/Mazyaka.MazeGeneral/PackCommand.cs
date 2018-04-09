using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mazyaka.MazeGeneral
{
    // Возможный тип команды
    public enum TypeCommand
    {
        // ServerTypes
        Error,
        Responce,
        GameIsStart,
        // ClientTypes
        Login, // +
        CreateGame, // +
        JoinGame, // +
        SendStartMaze, // ~
        SendStartPoint, // ~
        StartGame,
    }

    /// <summary>
    /// Передаваемый пакет данных
    /// </summary>
    [DataContract(IsReference = true)]
    public class PackCommand : TransmittedClass<PackCommand>
    {
        [DataMember]
        public TypeCommand Type { get; private set; } // Тип команды

        // TODO: Возможно, можно держать лист байтов
        [DataMember]
        public List<String> Args { get; private set; } = null; // список аргументов
        

        public String this[int index] => Args[index];

        public PackCommand(TypeCommand type)
        {
            this.Type = type;
            // Args = new List<string>();
        }

        public void AddArgument(String arg)
        {
            if (Args == null) Args = new List<string>();
            Args.Add(arg);
        }
    }
}
