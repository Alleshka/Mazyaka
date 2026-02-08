using Maze.GameLogic.Connections;
using Maze.GameLogic.GameMazeSite;
using Maze.MazeStructure;

namespace Maze.GameLogic.Actors
{
    public interface IActor
    {
        public GameMazeRoom CurrentRoom { get; }
        public MoveResult TryMove(IMazeGameConnection connection);
    }
}
