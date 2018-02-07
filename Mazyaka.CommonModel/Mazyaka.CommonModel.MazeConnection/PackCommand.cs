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
        /// <summary>
        /// Получить ID
        /// </summary>
        Login, 
        /// <summary>
        /// Создать игру
        /// </summary>
        CreateGame,
        /// <summary>
        /// Присоединиться к игре
        /// </summary>
        JoinGame,
        /// <summary>
        /// Выйти из сети
        /// </summary>
        Disconnect,
        /// <summary>
        /// Проверка наалась ли игра
        /// </summary>
        IsGameStart,
        /// <summary>
        /// Проверка мой ли ход
        /// </summary>
        IsMyStep,
        /// <summary>
        /// Сделать ход
        /// </summary>
        MoveObject,
        /// <summary>
        /// Ответ
        /// </summary>
        Response,
        /// <summary>
        /// Отправить лабиринт
        /// </summary>
        SendMaze
    }

    /// <summary>
    /// Передаваемый пакет данных
    /// </summary>
    [DataContract(IsReference = true)]
    public class PackCommand : TransmittedClass<PackCommand>
    {
        [DataMember]
        public TypeCommand type { get; private set; } // Тип команды
        [DataMember]
        public List<String> args { get; private set; } // список аргументов

        public String this[int index] => args[index];

        public PackCommand(TypeCommand type)
        {
            this.type = type;
            args = new List<string>();
        }

        public void AddArgument(String arg)
        {
            args.Add(arg);
        }
    }
}
