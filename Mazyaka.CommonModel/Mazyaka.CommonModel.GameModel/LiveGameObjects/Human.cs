using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.GameModel
{
    [DataContract(IsReference = true)]
    public class Human : LiveGameObject
    {
        public override void Action(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        public override void Move(MoveDirection direction)
        {
            throw new System.NotImplementedException();
        }
    }
}