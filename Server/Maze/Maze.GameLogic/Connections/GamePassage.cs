using Maze.GameLogic.GameMazeSite;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic.Connections
{
    public class GamePassage : BaseMazeGameConnection
    {
        public GamePassage(IMazeConnection connection) : base(connection)
        {
        }

        public override MoveResult TryMove(IActor actor, GameMazeRoom from)
        {
            return MoveResult.Success(from == MazeConnection.RoomA ? MazeConnection.RoomB : MazeConnection.RoomA);
        }
    }
}
