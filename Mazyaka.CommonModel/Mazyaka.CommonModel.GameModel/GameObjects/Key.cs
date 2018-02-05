using System;
using System.Runtime.Serialization;

namespace Mazyaka.CommonModel.GameModel
{

    [DataContract(IsReference = true)]
    public class Key : GameObject
    {
        public override void Action(GameObject obj)
        {
            throw new NotImplementedException();
        }
    }
}