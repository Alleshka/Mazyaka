using System.Runtime.Serialization;

namespace Mazyaka.MazeGeneral.MazeModel
{
    [DataContract (IsReference = true)]
    public class Exit : GameObject
    {
        public override void Action(GameObject obj)
        {
            throw new System.NotImplementedException();
        }
    }
}