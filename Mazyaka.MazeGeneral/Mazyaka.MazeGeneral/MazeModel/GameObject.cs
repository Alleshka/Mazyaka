using System;
using System.Runtime.Serialization;

namespace Mazyaka.MazeGeneral.MazeModel
{
    /// <summary>
    /// класс, от которого наследуются все игровые объекты
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(Point))]
    public abstract class GameObject
    {
        [DataMember]
        public Guid ID { get; set; } = Guid.NewGuid(); // Уникальный ID объекта

        [DataMember]
        public Point Position { get; set; }

        public abstract void Action(GameObject obj);
    }
}