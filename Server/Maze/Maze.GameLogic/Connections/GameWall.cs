using Maze.GameLogic.GameMazeSite;
using Maze.MazeStructure;
using Maze.MazeStructure.MazeSites;

namespace Maze.GameLogic.Connections
{
    public class GameWall : BaseMazeGameConnection
    {
        public virtual bool CanDestroy { get; }
        public virtual bool Destroyed { get; }

        public GameWall(IMazeConnection connection, bool destructable = true) : base (connection)
        {
            CanDestroy = destructable;
            Destroyed = false;
        }

        public override MoveResult TryMove(IActor actor, GameMazeRoom from)
        {
            if (CanDestroy && Destroyed)
            {
                return MoveResult.Failed(this.GetType().Name);
            }
            else
            {
                return MoveResult.Success(from == MazeConnection.RoomA ? MazeConnection.RoomB : MazeConnection.RoomA);
            }
        }
    }
}
