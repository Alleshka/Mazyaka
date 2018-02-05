using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.GameModel
{
    [DataContract(IsReference = true)]
    public class FalseKey : GameObject
    {
        public override void Action(GameObject obj)
        {
            throw new System.NotImplementedException();
        }
    }
}