using Maze.GameLogic.GameMazeSite;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic.Connections
{
    public interface IMazeGameConnection
    {
        public IMazeConnection MazeConnection { get; }
        MoveResult TryMove(IActor actor, GameMazeRoom from);
    }
}
