using System;
using MazeProject.General.GameObjects;

using System.Collections.Generic;
using System.Text;

namespace MazeProject.General.Mazes
{
    public class MazeArea
    {
        public Guid MazeID { get; private set; }
        public Guid Creator { get; private set; }

        private MazeStructure mazeStruct;

        public MazeArea()
        {
            MazeID = Guid.NewGuid();
            mazeStruct = null;
        }

        public void AddHero(AliveGameObject liveObject, PositionInMaze position)
        {
            var cell = mazeStruct[position];
            cell.AddGameObject(liveObject);
        }

        public bool ReplaceObject (Guid gameObjectID, MoveDirection direction)
        {
            var oldCell = FindCellWithObject(gameObjectID);
            var gameObject = oldCell.GetGameObject(gameObjectID);
            var newCell = GetNewCell(oldCell, direction);

            if (newCell is MazeCell)
            {
                oldCell.RemoveGameObject(gameObject);
                (newCell as MazeCell).AddGameObject(gameObject);
                return true;
            }
            else if (newCell is MazeWall)
            {
                (newCell as MazeWall).ShowWall();
                return false;
            }
            else
            {
                return false;
            }
        }

        // TODO: Вынести в статический клас?
        private MazeBlock GetNewCell(MazeCell oldCell, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Down: return oldCell.DOWN;
                case MoveDirection.Left: return oldCell.LEFT;
                case MoveDirection.Right: return oldCell.RIGHT;
                case MoveDirection.Up: return oldCell.UP;
                default: return null;
            }
        }

        // TODO: Пересмтреть проход по всем ячейкам
        private MazeCell FindCellWithObject(Guid gameObjectID)
        {
            for(int i=0; i<mazeStruct.MazeSize; i++)
            {
                for(int j=0; j<mazeStruct.MazeSize; j++)
                {
                    if (mazeStruct[i, j].GetGameObject(gameObjectID) != null) return mazeStruct[i, j];
                }
            }
            return null;
        }
        private MazeCell FindCellWithObject(BaseGameObject gameObject)
        {
            return FindCellWithObject(gameObject.ObjectID);
        }
    }
}
