using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.GameModel
{
    [DataContract(IsReference = true)]
    public enum MoveDirection { UP, RIGHT, DONW, LEFT }

    [DataContract(IsReference = true)]
    public abstract class LiveGameObject : GameObject
    {
        [DataMember]
        public double ActionPoint { get; set; }
        [DataMember]
        public double HealthPoint { get; set; }
        [DataMember]
        public double Observe { get; set; }

        public abstract void Move(MoveDirection direction);
    }
}