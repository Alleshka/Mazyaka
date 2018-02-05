using System;
using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.GameModel
{
    [DataContract(IsReference = true)]
    public abstract class GameObject
    {
        [DataMember]
        public Guid ID { get; set; } = Guid.NewGuid(); // Уникальный ID объекта

        [DataMember]
        public Point Position { get; set; }

        public abstract void Action(GameObject obj);
    }
}