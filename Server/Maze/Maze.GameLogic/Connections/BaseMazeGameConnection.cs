using Maze.GameLogic.GameMazeSite;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic.Connections
{
    public abstract class BaseMazeGameConnection : IMazeGameConnection
    {
        public IMazeConnection MazeConnection { get; private set; }

        public BaseMazeGameConnection(IMazeConnection connection)
        {
            MazeConnection = connection;
        }

        public abstract MoveResult TryMove(IActor actor, GameMazeRoom from);
    }
}
