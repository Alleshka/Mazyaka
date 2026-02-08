using Maze.GameLogic.GameObjects;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic.GameMazeSite
{
    public class GameMazeRoom
    {
        public IMazeRoom BaseRoom { get; set; }
        public List<IGameObject> Objects { get; }

        public GameMazeRoom(IMazeRoom baseRoom)
        {
            BaseRoom = baseRoom;
            Objects = new List<IGameObject>();
        }
    }
}
