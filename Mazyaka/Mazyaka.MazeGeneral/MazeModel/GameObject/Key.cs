using System;
using System.Runtime.Serialization;

namespace Mazyaka.MazeGeneral.MazeModel
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