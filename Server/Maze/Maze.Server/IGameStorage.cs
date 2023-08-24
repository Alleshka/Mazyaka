using Maze.GameLogic;
using Maze.MazeStructure.Interfaces;

namespace Maze.Server
{
    public interface IGameStorage
    {
        public Guid AddGame(IMazeGame game);
        public IMazeGame GetGame(Guid id);
    }
}
