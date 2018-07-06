using System;
using System.Collections.Generic;
using System.Text;
using MazeProject.General.Mazes.GameObjects;
using System.Linq;

namespace MazeProject.General.Mazes.MazeStructure
{
    public abstract class MazeBlock
    {

    }

    public class MazeWall : MazeBlock
    {
        public MazeWall() : base()
        {

        }

        public override string ToString()
        {
            return "W";
        }
    }

    public class Cell : MazeBlock
    {
        private List<BaseGameObject> gameObjectList;

        public MazeBlock Left { get; set; }
        public MazeBlock Right { get; set; }
        public MazeBlock Up { get; set; }
        public MazeBlock Down { get; set; }

        public Cell() : base()
        {
            gameObjectList = new List<BaseGameObject>();

            Left = null;
            Right = null;
            Up = null;
            Down = null;
        }

        public void AddObject(BaseGameObject gameObject)
        {
            if (!gameObjectList.Contains(gameObject)) gameObjectList.Add(gameObject);
        }

        public void RemoveObject(BaseGameObject gameObject)
        {
            gameObjectList.Remove(gameObject);
        }
        public void RemoveObject(Guid gameObjectID)
        {
            gameObjectList.RemoveAll(x => x.ID == gameObjectID);
        }

        public BaseGameObject GetObject(Guid gameObjectID)
        {
            return gameObjectList.Find(x => x.ID == gameObjectID);
        }

        public bool IsContainObject(Guid id)
        {
            return gameObjectList.Any(x => x.ID == id);
        }
    }
}
