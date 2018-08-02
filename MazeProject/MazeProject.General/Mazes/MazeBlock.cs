using System;
using System.Collections.Generic;
using System.Text;
using MazeProject.General.GameObjects;
using System.Linq;

namespace MazeProject.General.Mazes
{
    public abstract class MazeBlock
    {
        public Guid BlockID { get; private set; }

        public MazeBlock()
        {
            BlockID = Guid.NewGuid();
        }
    }

    public class MazeWall : MazeBlock
    {
        public bool IsVisible { get; private set; }
        
        public MazeWall () : base()
        {
            IsVisible = false;
        }

        public void ShowWall()
        {
            IsVisible = true;
        }
    }

    public class MazeCell : MazeBlock
    {
        public MazeBlock UP { get; private set; }
        public MazeBlock DOWN { get; private set; }
        public MazeBlock LEFT { get; private set; }
        public MazeBlock RIGHT { get; private set; }

        private List<BaseGameObject> gameObjectList;

        public MazeCell() : base ()
        {
            UP = null;
            DOWN = null;
            LEFT = null;
            RIGHT = null;
            gameObjectList = new List<BaseGameObject>();
        }

        public void AddGameObject(BaseGameObject gameObject)
        {
            gameObjectList.Add(gameObject);
        }

        public void RemoveGameObject(BaseGameObject gameObject)
        {
            gameObjectList.Remove(gameObject);
        }

        public void RemoveGameObject(Guid objectID)
        {
            gameObjectList.RemoveAll(x => x.ObjectID == objectID);
        }

        public BaseGameObject GetGameObject(Guid id)
        {
            if (!gameObjectList.Any(x => x.ObjectID == id))
            {
                return gameObjectList.Find(x => x.ObjectID == id);
            }
            else return null;
        }
    }
}
